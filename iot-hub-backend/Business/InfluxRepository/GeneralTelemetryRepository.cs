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
using Microsoft.EntityFrameworkCore;

namespace Business.InfluxRepository
{
    public class GeneralTelemetryRepository : IInfluxRepository
    {
        private readonly string BUCKET = "general-telemetry-bucket";
        private readonly string ORG = "my-org";

        private readonly InfluxDBClient _influxDBClient;


        public GeneralTelemetryRepository(InfluxRepositoryConnection connection)
        {
            _influxDBClient = new InfluxDBClient(connection.ServerAddress, connection.Username, connection.Password);
        }


        public void Add(GeneralTelemetry log)
        {
            using var writeApi = _influxDBClient.GetWriteApi();
            writeApi.WriteMeasurement(log, WritePrecision.Ns, BUCKET, ORG);
        }

        public void Add(List<GeneralTelemetry> logs)
        {
            using var writeApi = _influxDBClient.GetWriteApi();
            writeApi.WriteMeasurements(logs, WritePrecision.Ns, BUCKET, ORG);
        }

        public List<GeneralTelemetry> GetAll(Guid userId)
        {

            var queryApi = _influxDBClient.GetQueryApiSync();

            var query = from s in InfluxDBQueryable<GeneralTelemetry>.Queryable(BUCKET, ORG, queryApi)
                        where s.UserId == userId
                        select s;

            var logs = query.ToList();


            return logs;

        }

        public List<GeneralTelemetry> GetAll(Guid userId, int limit)
        {
            var queryApi = _influxDBClient.GetQueryApiSync();

            var query = from s in InfluxDBQueryable<GeneralTelemetry>.Queryable(BUCKET, ORG, queryApi)
                        where s.UserId == userId
                        select s;

            var logs = query.Take(limit).ToList();


            return logs;
        }

        public List<GeneralTelemetry> Get(Guid userId, string fieldName, DateTime sinceUTC, DateTime toUTC)
        {
            if (sinceUTC > toUTC)
            {
                (toUTC, sinceUTC) = (sinceUTC, toUTC);
            }

            if (sinceUTC == DateTime.MinValue)
            {
                sinceUTC = DateTime.UtcNow.AddDays(-1);
            }

            if (toUTC == DateTime.MinValue)
            {
                toUTC = DateTime.UtcNow;
            }

            var queryApi = _influxDBClient.GetQueryApiSync();

            var query = from s in InfluxDBQueryable<GeneralTelemetry>.Queryable(BUCKET, ORG, queryApi)
                        where s.UserId == userId
                        && s.FieldName == fieldName
                        && s.DateUTC >= sinceUTC && s.DateUTC <= toUTC
                        select s;

            var telemetries = query.ToList();

            return telemetries;
        }

        public List<GeneralTelemetry>? GetLastN(Guid userId, string fieldName, int n)
        {
            var queryApi = _influxDBClient.GetQueryApiSync();

            var query = from s in InfluxDBQueryable<GeneralTelemetry>.Queryable(BUCKET, ORG, queryApi)
                        where s.UserId == userId
                        && s.FieldName == fieldName
                        orderby s.DateUTC descending
                        select s;

            var telemetries = query.Take(n).ToList();


            return telemetries;
        }

        public GeneralTelemetry? GetLatest(Guid userId, string fieldName)
        {
            var queryApi = _influxDBClient.GetQueryApiSync();

            var query = from s in InfluxDBQueryable<GeneralTelemetry>.Queryable(BUCKET, ORG, queryApi)
                        where s.UserId == userId
                        && s.FieldName == fieldName
                        orderby s.DateUTC descending
                        select s;

            var telemetries = query.Take(1).ToList();

            if(telemetries.Count > 0)
            {
                return telemetries[0];
            }

            return null;
        }
    }
}
