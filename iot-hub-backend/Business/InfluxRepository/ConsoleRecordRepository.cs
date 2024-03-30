using Business.Interface;
using Domain.InfluxDB;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Linq;
using InfluxDB.Client;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.InfluxRepository
{
    public class ConsoleRecordRepository : IInfluxRepository
    {

        private readonly string BUCKET = "console-record-bucket";
        private readonly string ORG = "my-org";

        private readonly InfluxDBClient _influxDBClient;


        public ConsoleRecordRepository(InfluxRepositoryConnection connection)
        {
            _influxDBClient = new InfluxDBClient(connection.ServerAddress, connection.Username, connection.Password);
        }


        public void Add(ConsoleRecord log)
        {
            using var writeApi = _influxDBClient.GetWriteApi();
            writeApi.WriteMeasurement(log, WritePrecision.Ns, BUCKET, ORG);
        }

        public void Add(List<ConsoleRecord> logs)
        {
            using var writeApi = _influxDBClient.GetWriteApi();
            writeApi.WriteMeasurements(logs, WritePrecision.Ns, BUCKET, ORG);
        }

        public List<ConsoleRecord> GetAll(Guid deviceId)
        {

            var queryApi = _influxDBClient.GetQueryApiSync();

            var query = from s in InfluxDBQueryable<ConsoleRecord>.Queryable(BUCKET, ORG, queryApi)
                        where s.DeviceId == deviceId
                        orderby s.DateUTC ascending
                        select s;

            var logs = query.ToList();


            return logs;

        }

        public List<ConsoleRecord> GetAll(Guid deviceId, int limit)
        {
            var queryApi = _influxDBClient.GetQueryApiSync();

            var query = from s in InfluxDBQueryable<ConsoleRecord>.Queryable(BUCKET, ORG, queryApi)
                        where s.DeviceId == deviceId
                        select s;

            var logs = query.Take(limit).ToList();


            return logs;
        }
    }
}
