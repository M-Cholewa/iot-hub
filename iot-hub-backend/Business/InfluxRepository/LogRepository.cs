using Business.Interface;
using Domain.InfluxDB;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core;
using InfluxDB.Client.Linq;
using Microsoft.EntityFrameworkCore;
using Remotion.Linq.Parsing.Structure.NodeTypeProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.InfluxRepository
{
    public class LogRepository : IInfluxRepository
    {

        private readonly string BUCKET = "log-bucket";
        private readonly string ORG = "my-org";

        private readonly InfluxDBClient _influxDBClient;


        public LogRepository(InfluxRepositoryConnection connection)
        {
            _influxDBClient = new InfluxDBClient(connection.ServerAddress, connection.Username, connection.Password);
        }


        public void Add(Log log)
        {
            using var writeApi = _influxDBClient.GetWriteApi();
            writeApi.WriteMeasurement(log, WritePrecision.Ns, BUCKET, ORG);
        }

        public void Add(List<Log> logs)
        {
            using var writeApi = _influxDBClient.GetWriteApi();
            writeApi.WriteMeasurements(logs, WritePrecision.Ns, BUCKET, ORG);
        }

        public List<Log> GetAll(Guid deviceId)
        {

            var queryApi = _influxDBClient.GetQueryApiSync();

            var query = from s in InfluxDBQueryable<Log>.Queryable(BUCKET, ORG, queryApi)
                        where s.DeviceId == deviceId
                        select s;

            var logs = query.ToList();


            return logs.OrderBy(x => x.DateUTC).Reverse().ToList();

        }

        public List<Log> GetAll(Guid deviceId, int limit)
        {
            var queryApi = _influxDBClient.GetQueryApiSync();

            var query = from s in InfluxDBQueryable<Log>.Queryable(BUCKET, ORG, queryApi)
                        where s.DeviceId == deviceId
                        //orderby s.DateUTC descending
                        select s;

            var logs = query.TakeLast(limit).ToList();
            var ret = logs.OrderBy(x => x.DateUTC).Reverse().Take(limit).ToList();


            return ret;
        }

        public List<Log> GetAll(Guid deviceId, DateTime sinceUTC, DateTime toUTC)
        {
            var queryApi = _influxDBClient.GetQueryApiSync();

            var query = from s in InfluxDBQueryable<Log>.Queryable(BUCKET, ORG, queryApi)
                        where s.DeviceId == deviceId
                        && s.DateUTC >= sinceUTC && s.DateUTC <= toUTC
                        select s;

            var logs = query.ToList();


            return logs.OrderBy(x => x.DateUTC).Reverse().ToList();
        }

        public async Task DeleteAll(Guid deviceId)
        {
            var minDeleteDt = new DateTime(2023, 1, 1);

            var deleteApi = _influxDBClient.GetDeleteApi();
            await deleteApi.Delete(minDeleteDt, DateTime.UtcNow, $"DeviceId=\"{deviceId}\"", BUCKET, ORG);
        }



    }
}
