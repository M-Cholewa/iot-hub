--
-- PostgreSQL database dump
--

-- Dumped from database version 16.0 (Debian 16.0-1.pgdg120+1)
-- Dumped by pg_dump version 16.0

-- Started on 2023-11-22 21:47:34

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

\c postgres

DROP DATABASE IF EXISTS iothub;
--
-- TOC entry 3399 (class 1262 OID 16385)
-- Name: iothub; Type: DATABASE; Schema: -; Owner: dbuser
--

CREATE DATABASE iothub WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.utf8';


ALTER DATABASE iothub OWNER TO dbuser;

\connect iothub

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

--
-- TOC entry 5 (class 2615 OID 2200)
-- Name: public; Type: SCHEMA; Schema: -; Owner: pg_database_owner
--

CREATE SCHEMA IF NOT EXISTS public;


ALTER SCHEMA public OWNER TO pg_database_owner;

--
-- TOC entry 3400 (class 0 OID 0)
-- Dependencies: 5
-- Name: SCHEMA public; Type: COMMENT; Schema: -; Owner: pg_database_owner
--

COMMENT ON SCHEMA public IS 'standard public schema';


SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- TOC entry 215 (class 1259 OID 16386)
-- Name: DeviceUser; Type: TABLE; Schema: public; Owner: dbuser
--

CREATE TABLE public."DeviceUser" (
    "DevicesId" uuid NOT NULL,
    "UserId" uuid NOT NULL
);


ALTER TABLE public."DeviceUser" OWNER TO dbuser;

--
-- TOC entry 216 (class 1259 OID 16389)
-- Name: Devices; Type: TABLE; Schema: public; Owner: dbuser
--

CREATE TABLE public."Devices" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "DeviceTwin" character varying(4096),
    "MQTTUserClientID" uuid
);


ALTER TABLE public."Devices" OWNER TO dbuser;

--
-- TOC entry 217 (class 1259 OID 16394)
-- Name: MQTTUser; Type: TABLE; Schema: public; Owner: dbuser
--

CREATE TABLE public."MQTTUser" (
    "ClientID" uuid NOT NULL,
    "Username" character varying(200) NOT NULL,
    "PasswordHash" character varying(200) NOT NULL
);


ALTER TABLE public."MQTTUser" OWNER TO dbuser;

--
-- TOC entry 218 (class 1259 OID 16397)
-- Name: RoleUser; Type: TABLE; Schema: public; Owner: dbuser
--

CREATE TABLE public."RoleUser" (
    "RolesId" uuid NOT NULL,
    "UserId" uuid NOT NULL
);


ALTER TABLE public."RoleUser" OWNER TO dbuser;

--
-- TOC entry 219 (class 1259 OID 16400)
-- Name: Roles; Type: TABLE; Schema: public; Owner: dbuser
--

CREATE TABLE public."Roles" (
    "Id" uuid NOT NULL,
    "Key" character varying(200) NOT NULL
);


ALTER TABLE public."Roles" OWNER TO dbuser;

--
-- TOC entry 220 (class 1259 OID 16403)
-- Name: Users; Type: TABLE; Schema: public; Owner: dbuser
--

CREATE TABLE public."Users" (
    "Id" uuid NOT NULL,
    "Email" character varying(200) NOT NULL,
    "PasswordHash" character varying(200) NOT NULL
);


ALTER TABLE public."Users" OWNER TO dbuser;

--
-- TOC entry 3388 (class 0 OID 16386)
-- Dependencies: 215
-- Data for Name: DeviceUser; Type: TABLE DATA; Schema: public; Owner: dbuser
--

INSERT INTO public."DeviceUser" ("DevicesId", "UserId") VALUES ('6220654d-cfdd-4ab6-9a62-60c31ed307ab', '5a9516ad-ffc7-4f4b-be15-c9d63ebf1b25');


--
-- TOC entry 3389 (class 0 OID 16389)
-- Dependencies: 216
-- Data for Name: Devices; Type: TABLE DATA; Schema: public; Owner: dbuser
--

INSERT INTO public."Devices" ("Id", "Name", "DeviceTwin", "MQTTUserClientID") VALUES ('6220654d-cfdd-4ab6-9a62-60c31ed307ab', 'Room-ESP32', NULL, 'd201ab30-a86e-41af-8a4d-7589109c7fa8');


--
-- TOC entry 3390 (class 0 OID 16394)
-- Dependencies: 217
-- Data for Name: MQTTUser; Type: TABLE DATA; Schema: public; Owner: dbuser
--

INSERT INTO public."MQTTUser" ("ClientID", "Username", "PasswordHash") VALUES ('4af771e3-b76e-4e96-84b9-fb79f0092cde', 'r?A&W>cX2j?KG%/oV!ch8[IcbQ^jJ$', 'AQAAAAIAAYagAAAAEIVHQICCWipcnbTkeYgV3CI9zCrx9H+JIpvOcSUHcQZsxbloV+JyQriVgn79XcAgIg==');
INSERT INTO public."MQTTUser" ("ClientID", "Username", "PasswordHash") VALUES ('d201ab30-a86e-41af-8a4d-7589109c7fa8', 'NFG^QNpaZ0I9C4c/)6aPhja!a0&5IA', 'AQAAAAIAAYagAAAAEJdOeQ0DVpnSNuIbIxdsGQXOha9T3py3dGlG2JtpGBumol4PZlalqAHf+UGdAAE0EA==');
INSERT INTO public."MQTTUser" ("ClientID", "Username", "PasswordHash") VALUES ('19a1805b-9d19-4f46-a34a-d055dde12560', 'MessageProcessingService', 'AQAAAAIAAYagAAAAECjoEVq2UDs4JV9mSRNoSbrZXOGSUalPiXYViLsblUAqw4cRDWd6gE3GzGO9UoF/KA==');
INSERT INTO public."MQTTUser" ("ClientID", "Username", "PasswordHash") VALUES ('64f7f094-e0b9-4c52-935e-fd9d442d655e', 'IotHubApi', 'AQAAAAIAAYagAAAAEHvYVwOoKiYdHRjZDntVoYimP5h4wpwLzsZNN0X1K6vXBfMt7FQeRDBuUFQmZRpm3A==');


--
-- TOC entry 3391 (class 0 OID 16397)
-- Dependencies: 218
-- Data for Name: RoleUser; Type: TABLE DATA; Schema: public; Owner: dbuser
--

INSERT INTO public."RoleUser" ("RolesId", "UserId") VALUES ('c6438e61-2db3-47e6-97e4-ed9d28043722', '5a9516ad-ffc7-4f4b-be15-c9d63ebf1b25');
INSERT INTO public."RoleUser" ("RolesId", "UserId") VALUES ('1e009e3d-dd99-4956-830c-e489268410f3', '8b42e4a5-86cf-4b1e-a625-a027beedc6f6');
INSERT INTO public."RoleUser" ("RolesId", "UserId") VALUES ('c6438e61-2db3-47e6-97e4-ed9d28043722', '8b42e4a5-86cf-4b1e-a625-a027beedc6f6');


--
-- TOC entry 3392 (class 0 OID 16400)
-- Dependencies: 219
-- Data for Name: Roles; Type: TABLE DATA; Schema: public; Owner: dbuser
--

INSERT INTO public."Roles" ("Id", "Key") VALUES ('c6438e61-2db3-47e6-97e4-ed9d28043722', 'USER');
INSERT INTO public."Roles" ("Id", "Key") VALUES ('1e009e3d-dd99-4956-830c-e489268410f3', 'ADMIN');


--
-- TOC entry 3393 (class 0 OID 16403)
-- Dependencies: 220
-- Data for Name: Users; Type: TABLE DATA; Schema: public; Owner: dbuser
--

INSERT INTO public."Users" ("Id", "Email", "PasswordHash") VALUES ('8b42e4a5-86cf-4b1e-a625-a027beedc6f6', 'admin', 'AQAAAAIAAYagAAAAEOHDrX8MXHDWqbptLoclT+cFLff+0IrTmcDjmkZqE60DwoY5sxe6/gezb/21OfZuAg==');
INSERT INTO public."Users" ("Id", "Email", "PasswordHash") VALUES ('5a9516ad-ffc7-4f4b-be15-c9d63ebf1b25', 'user', 'AQAAAAIAAYagAAAAELdi+ZwxG7uwHgjW96b+EzgHiadp067MIJJPp5fVsCT68Yh2nhvfip+zWYC1jwvSig==');


--
-- TOC entry 3224 (class 2606 OID 16407)
-- Name: DeviceUser PK_DeviceUser; Type: CONSTRAINT; Schema: public; Owner: dbuser
--

ALTER TABLE ONLY public."DeviceUser"
    ADD CONSTRAINT "PK_DeviceUser" PRIMARY KEY ("DevicesId", "UserId");


--
-- TOC entry 3227 (class 2606 OID 16409)
-- Name: Devices PK_Devices; Type: CONSTRAINT; Schema: public; Owner: dbuser
--

ALTER TABLE ONLY public."Devices"
    ADD CONSTRAINT "PK_Devices" PRIMARY KEY ("Id");


--
-- TOC entry 3230 (class 2606 OID 16411)
-- Name: MQTTUser PK_MQTTUser; Type: CONSTRAINT; Schema: public; Owner: dbuser
--

ALTER TABLE ONLY public."MQTTUser"
    ADD CONSTRAINT "PK_MQTTUser" PRIMARY KEY ("ClientID");


--
-- TOC entry 3233 (class 2606 OID 16413)
-- Name: RoleUser PK_RoleUser; Type: CONSTRAINT; Schema: public; Owner: dbuser
--

ALTER TABLE ONLY public."RoleUser"
    ADD CONSTRAINT "PK_RoleUser" PRIMARY KEY ("RolesId", "UserId");


--
-- TOC entry 3236 (class 2606 OID 16415)
-- Name: Roles PK_Roles; Type: CONSTRAINT; Schema: public; Owner: dbuser
--

ALTER TABLE ONLY public."Roles"
    ADD CONSTRAINT "PK_Roles" PRIMARY KEY ("Id");


--
-- TOC entry 3239 (class 2606 OID 16417)
-- Name: Users PK_Users; Type: CONSTRAINT; Schema: public; Owner: dbuser
--

ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "PK_Users" PRIMARY KEY ("Id");


--
-- TOC entry 3222 (class 1259 OID 16418)
-- Name: IX_DeviceUser_UserId; Type: INDEX; Schema: public; Owner: dbuser
--

CREATE INDEX "IX_DeviceUser_UserId" ON public."DeviceUser" USING btree ("UserId");


--
-- TOC entry 3225 (class 1259 OID 16419)
-- Name: IX_Devices_MQTTUserClientID; Type: INDEX; Schema: public; Owner: dbuser
--

CREATE INDEX "IX_Devices_MQTTUserClientID" ON public."Devices" USING btree ("MQTTUserClientID");


--
-- TOC entry 3228 (class 1259 OID 16420)
-- Name: IX_MQTTUser_Username; Type: INDEX; Schema: public; Owner: dbuser
--

CREATE UNIQUE INDEX "IX_MQTTUser_Username" ON public."MQTTUser" USING btree ("Username");


--
-- TOC entry 3231 (class 1259 OID 16421)
-- Name: IX_RoleUser_UserId; Type: INDEX; Schema: public; Owner: dbuser
--

CREATE INDEX "IX_RoleUser_UserId" ON public."RoleUser" USING btree ("UserId");


--
-- TOC entry 3234 (class 1259 OID 16422)
-- Name: IX_Roles_Key; Type: INDEX; Schema: public; Owner: dbuser
--

CREATE UNIQUE INDEX "IX_Roles_Key" ON public."Roles" USING btree ("Key");


--
-- TOC entry 3237 (class 1259 OID 16423)
-- Name: IX_Users_Email; Type: INDEX; Schema: public; Owner: dbuser
--

CREATE UNIQUE INDEX "IX_Users_Email" ON public."Users" USING btree ("Email");


--
-- TOC entry 3240 (class 2606 OID 16424)
-- Name: DeviceUser FK_DeviceUser_Devices_DevicesId; Type: FK CONSTRAINT; Schema: public; Owner: dbuser
--

ALTER TABLE ONLY public."DeviceUser"
    ADD CONSTRAINT "FK_DeviceUser_Devices_DevicesId" FOREIGN KEY ("DevicesId") REFERENCES public."Devices"("Id") ON DELETE CASCADE;


--
-- TOC entry 3241 (class 2606 OID 16429)
-- Name: DeviceUser FK_DeviceUser_Users_UserId; Type: FK CONSTRAINT; Schema: public; Owner: dbuser
--

ALTER TABLE ONLY public."DeviceUser"
    ADD CONSTRAINT "FK_DeviceUser_Users_UserId" FOREIGN KEY ("UserId") REFERENCES public."Users"("Id") ON DELETE CASCADE;


--
-- TOC entry 3242 (class 2606 OID 16434)
-- Name: Devices FK_Devices_MQTTUser_MQTTUserClientID; Type: FK CONSTRAINT; Schema: public; Owner: dbuser
--

ALTER TABLE ONLY public."Devices"
    ADD CONSTRAINT "FK_Devices_MQTTUser_MQTTUserClientID" FOREIGN KEY ("MQTTUserClientID") REFERENCES public."MQTTUser"("ClientID");


--
-- TOC entry 3243 (class 2606 OID 16439)
-- Name: RoleUser FK_RoleUser_Roles_RolesId; Type: FK CONSTRAINT; Schema: public; Owner: dbuser
--

ALTER TABLE ONLY public."RoleUser"
    ADD CONSTRAINT "FK_RoleUser_Roles_RolesId" FOREIGN KEY ("RolesId") REFERENCES public."Roles"("Id") ON DELETE CASCADE;


--
-- TOC entry 3244 (class 2606 OID 16444)
-- Name: RoleUser FK_RoleUser_Users_UserId; Type: FK CONSTRAINT; Schema: public; Owner: dbuser
--

ALTER TABLE ONLY public."RoleUser"
    ADD CONSTRAINT "FK_RoleUser_Users_UserId" FOREIGN KEY ("UserId") REFERENCES public."Users"("Id") ON DELETE CASCADE;


--
-- TOC entry 3401 (class 0 OID 0)
-- Dependencies: 5
-- Name: SCHEMA public; Type: ACL; Schema: -; Owner: pg_database_owner
--

REVOKE USAGE ON SCHEMA public FROM PUBLIC;


-- Completed on 2023-11-24 21:20:18

--
-- PostgreSQL database dump complete
--

