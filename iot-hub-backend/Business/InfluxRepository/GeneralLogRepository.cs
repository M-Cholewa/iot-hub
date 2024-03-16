using Domain.InfluxDB;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Linq;
using InfluxDB.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Business.Interface;

namespace Business.InfluxRepository
{
    public class GeneralLogRepository : IInfluxRepository
    {
        private readonly string BUCKET = "general-log-bucket";
        private readonly string ORG = "my-org";

        private readonly InfluxDBClient _influxDBClient;


        public GeneralLogRepository(InfluxRepositoryConnection connection)
        {
            _influxDBClient = new InfluxDBClient(connection.ServerAddress, connection.Username, connection.Password);
        }


        public void Add(GeneralLog log)
        {
            using var writeApi = _influxDBClient.GetWriteApi();
            writeApi.WriteMeasurement(log, WritePrecision.Ns, BUCKET, ORG);
        }

        public void Add(List<GeneralLog> logs)
        {
            using var writeApi = _influxDBClient.GetWriteApi();
            writeApi.WriteMeasurements(logs, WritePrecision.Ns, BUCKET, ORG);
        }

        public List<GeneralLog> GetAll(Guid deviceId)
        {

            var queryApi = _influxDBClient.GetQueryApiSync();

            var query = from s in InfluxDBQueryable<GeneralLog>.Queryable(BUCKET, ORG, queryApi)
                        where s.DeviceId == deviceId
                        select s;

            var logs = query.ToList();


            return logs;

        }

        public List<GeneralLog> GetAll(Guid deviceId, int limit)
        {
            var queryApi = _influxDBClient.GetQueryApiSync();

            var query = from s in InfluxDBQueryable<GeneralLog>.Queryable(BUCKET, ORG, queryApi)
                        where s.DeviceId == deviceId
                        select s;

            var logs = query.Take(limit).ToList();


            return logs;
        }

        public List<GeneralLog> GetAll(Guid deviceId, DateTime sinceUTC, DateTime toUTC)
        {
            var queryApi = _influxDBClient.GetQueryApiSync();

            var query = from s in InfluxDBQueryable<GeneralLog>.Queryable(BUCKET, ORG, queryApi)
                        where s.DeviceId == deviceId
                        && s.DateUTC >= sinceUTC && s.DateUTC <= toUTC
                        select s;

            var logs = query.ToList();


            return logs;
        }


        public GeneralLog? GetLastN(Guid deviceId, int n)
        {
            var queryApi = _influxDBClient.GetQueryApiSync();

            var query = from s in InfluxDBQueryable<GeneralLog>.Queryable(BUCKET, ORG, queryApi)
                        where s.DeviceId == deviceId
                        orderby s.DateUTC descending
                        select s;

            var logs = query.Take(n).ToList();

            if (logs.Count > 0)
            {
                return logs[0];
            }

            return null;
        }
    }
}
