using Business.Interface;
using InfluxDB.Client;
using InfluxDB.Client.Api.Domain;
using InfluxDB.Client.Core.Flux.Domain;
using InfluxDB.Client.Flux;
using InfluxDB.Client.Writes;
using Common;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using Microsoft.VisualBasic;
using InfluxDB.Client.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Routing;
using InfluxDB.Client.Configurations;
using System.Net.Sockets;
using Domain.InfluxDB;
using Domain.Core;

namespace Business.InfluxRepository
{
    public class TelemetryRepository : IInfluxRepository
    {

        private readonly string BUCKET = "telemetry-bucket";
        private readonly string ORG = "my-org";

        private readonly InfluxDBClient _influxDBClient;


        public TelemetryRepository(InfluxRepositoryConnection connection)
        {
            _influxDBClient = new InfluxDBClient(connection.ServerAddress, connection.Username, connection.Password);
        }

        public void Add(Telemetry telemetry)
        {
            using var writeApi = _influxDBClient.GetWriteApi();
            writeApi.WriteMeasurement(telemetry, WritePrecision.Ns, BUCKET, ORG);
        }

        public void Add(List<Telemetry> telemetries)
        {
            using var writeApi = _influxDBClient.GetWriteApi();
            writeApi.WriteMeasurements(telemetries, WritePrecision.Ns, BUCKET, ORG);
        }

        public List<Telemetry> Get(Guid deviceId, string fieldName, DateTime sinceUTC, DateTime toUTC)
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

            var query = from s in InfluxDBQueryable<Telemetry>.Queryable(BUCKET, ORG, queryApi)
                        where s.DeviceId == deviceId
                        && s.FieldName == fieldName
                        && s.DateUTC >= sinceUTC && s.DateUTC <= toUTC
                        select s;

            var telemetries = query.ToList();

            return telemetries;
        }

        public Telemetry? GetLatest(Guid deviceId, string fieldName)
        {
            var queryApi = _influxDBClient.GetQueryApiSync();

            var query = from s in InfluxDBQueryable<Telemetry>.Queryable(BUCKET, ORG, queryApi)
                        where s.DeviceId == deviceId
                        && s.FieldName == fieldName
                        orderby s.DateUTC descending
                        select s;

            var telemetries = query.Take(1).ToList();

            if (telemetries.Count > 0)
            {
                return telemetries[0];
            }

            return null;
        }

        public async Task<List<string>> GetFieldNames(Guid deviceId)
        {
            var query = $"from(bucket: \"{BUCKET}\")  " +
                         "|> range(start: 0)  " +
                        $"|> filter(fn: (r) => (r.DeviceId == \"{deviceId}\"))  " +
                         "|> group(columns: [\"FieldName\"]) " +
                         "|> distinct(column: \"_measurement\")";



            var tables = await _influxDBClient.GetQueryApi()
                .QueryAsync(new Query(query: query, dialect: QueryApi.Dialect), org: ORG);

            var strings = new List<string>();

            foreach (var record in tables.SelectMany(table => table.Records))
                strings.Add(record.GetValue().ToString() ?? "");


            return strings;

        }

        public async Task<List<Telemetry>> GetTelemetryMap(Guid deviceId)
        {
            var query = $"from(bucket: \"{BUCKET}\")" +
                        "|> range(start: 0)  " +
                        $"|> filter(fn: (r) => (r.DeviceId == \"{deviceId}\"))  " +
                        "|> sort(columns: [\"_time\"], desc: true)" +
                        "|> unique(column: \"_measurement\")";

            List<Telemetry> telemetries = new();

            var tables = await _influxDBClient.GetQueryApi()
                .QueryAsync(new Query(query: query, dialect: QueryApi.Dialect), org: ORG);


            foreach (var table in tables)
            {
                if (table.Records[0].GetValue() is Telemetry t)
                {
                    telemetries.Add(t);
                }
            }

            return telemetries;
        }

    }
}
