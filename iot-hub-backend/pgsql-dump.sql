PGDMP  
    9            
    {            iothub    16.0 (Debian 16.0-1.pgdg120+1)    16.0 #    D           0    0    ENCODING    ENCODING        SET client_encoding = 'UTF8';
                      false            E           0    0 
   STDSTRINGS 
   STDSTRINGS     (   SET standard_conforming_strings = 'on';
                      false            F           0    0 
   SEARCHPATH 
   SEARCHPATH     8   SELECT pg_catalog.set_config('search_path', '', false);
                      false            G           1262    16384    iothub    DATABASE     q   CREATE DATABASE iothub WITH TEMPLATE = template0 ENCODING = 'UTF8' LOCALE_PROVIDER = libc LOCALE = 'en_US.utf8';
    DROP DATABASE iothub;
                dbuser    false                        2615    2200    public    SCHEMA        CREATE SCHEMA public;
    DROP SCHEMA public;
                pg_database_owner    false            H           0    0    SCHEMA public    COMMENT     6   COMMENT ON SCHEMA public IS 'standard public schema';
                   pg_database_owner    false    4            �            1259    16385 
   DeviceUser    TABLE     `   CREATE TABLE public."DeviceUser" (
    "DevicesId" uuid NOT NULL,
    "UserId" uuid NOT NULL
);
     DROP TABLE public."DeviceUser";
       public         heap    dbuser    false    4            �            1259    16388    Devices    TABLE     �   CREATE TABLE public."Devices" (
    "Id" uuid NOT NULL,
    "Name" text NOT NULL,
    "DeviceTwin" character varying(4096),
    "MQTTUserClientID" uuid
);
    DROP TABLE public."Devices";
       public         heap    dbuser    false    4            �            1259    16393    MQTTUser    TABLE     �   CREATE TABLE public."MQTTUser" (
    "ClientID" uuid NOT NULL,
    "Username" character varying(200) NOT NULL,
    "PasswordHash" character varying(200) NOT NULL
);
    DROP TABLE public."MQTTUser";
       public         heap    dbuser    false    4            �            1259    16396    RoleUser    TABLE     \   CREATE TABLE public."RoleUser" (
    "RolesId" uuid NOT NULL,
    "UserId" uuid NOT NULL
);
    DROP TABLE public."RoleUser";
       public         heap    dbuser    false    4            �            1259    16399    Roles    TABLE     c   CREATE TABLE public."Roles" (
    "Id" uuid NOT NULL,
    "Key" character varying(200) NOT NULL
);
    DROP TABLE public."Roles";
       public         heap    dbuser    false    4            �            1259    16402    Users    TABLE     �   CREATE TABLE public."Users" (
    "Id" uuid NOT NULL,
    "Email" character varying(200) NOT NULL,
    "PasswordHash" character varying(200) NOT NULL
);
    DROP TABLE public."Users";
       public         heap    dbuser    false    4            <          0    16385 
   DeviceUser 
   TABLE DATA                 public          dbuser    false    215   �$       =          0    16388    Devices 
   TABLE DATA                 public          dbuser    false    216   �%       >          0    16393    MQTTUser 
   TABLE DATA                 public          dbuser    false    217   E&       ?          0    16396    RoleUser 
   TABLE DATA                 public          dbuser    false    218   :'       @          0    16399    Roles 
   TABLE DATA                 public          dbuser    false    219   (       A          0    16402    Users 
   TABLE DATA                 public          dbuser    false    220   �(       �           2606    16406    DeviceUser PK_DeviceUser 
   CONSTRAINT     m   ALTER TABLE ONLY public."DeviceUser"
    ADD CONSTRAINT "PK_DeviceUser" PRIMARY KEY ("DevicesId", "UserId");
 F   ALTER TABLE ONLY public."DeviceUser" DROP CONSTRAINT "PK_DeviceUser";
       public            dbuser    false    215    215            �           2606    16408    Devices PK_Devices 
   CONSTRAINT     V   ALTER TABLE ONLY public."Devices"
    ADD CONSTRAINT "PK_Devices" PRIMARY KEY ("Id");
 @   ALTER TABLE ONLY public."Devices" DROP CONSTRAINT "PK_Devices";
       public            dbuser    false    216            �           2606    16410    MQTTUser PK_MQTTUser 
   CONSTRAINT     ^   ALTER TABLE ONLY public."MQTTUser"
    ADD CONSTRAINT "PK_MQTTUser" PRIMARY KEY ("ClientID");
 B   ALTER TABLE ONLY public."MQTTUser" DROP CONSTRAINT "PK_MQTTUser";
       public            dbuser    false    217            �           2606    16412    RoleUser PK_RoleUser 
   CONSTRAINT     g   ALTER TABLE ONLY public."RoleUser"
    ADD CONSTRAINT "PK_RoleUser" PRIMARY KEY ("RolesId", "UserId");
 B   ALTER TABLE ONLY public."RoleUser" DROP CONSTRAINT "PK_RoleUser";
       public            dbuser    false    218    218            �           2606    16414    Roles PK_Roles 
   CONSTRAINT     R   ALTER TABLE ONLY public."Roles"
    ADD CONSTRAINT "PK_Roles" PRIMARY KEY ("Id");
 <   ALTER TABLE ONLY public."Roles" DROP CONSTRAINT "PK_Roles";
       public            dbuser    false    219            �           2606    16416    Users PK_Users 
   CONSTRAINT     R   ALTER TABLE ONLY public."Users"
    ADD CONSTRAINT "PK_Users" PRIMARY KEY ("Id");
 <   ALTER TABLE ONLY public."Users" DROP CONSTRAINT "PK_Users";
       public            dbuser    false    220            �           1259    16417    IX_DeviceUser_UserId    INDEX     S   CREATE INDEX "IX_DeviceUser_UserId" ON public."DeviceUser" USING btree ("UserId");
 *   DROP INDEX public."IX_DeviceUser_UserId";
       public            dbuser    false    215            �           1259    16418    IX_Devices_MQTTUserClientID    INDEX     a   CREATE INDEX "IX_Devices_MQTTUserClientID" ON public."Devices" USING btree ("MQTTUserClientID");
 1   DROP INDEX public."IX_Devices_MQTTUserClientID";
       public            dbuser    false    216            �           1259    16419    IX_MQTTUser_Username    INDEX     Z   CREATE UNIQUE INDEX "IX_MQTTUser_Username" ON public."MQTTUser" USING btree ("Username");
 *   DROP INDEX public."IX_MQTTUser_Username";
       public            dbuser    false    217            �           1259    16420    IX_RoleUser_UserId    INDEX     O   CREATE INDEX "IX_RoleUser_UserId" ON public."RoleUser" USING btree ("UserId");
 (   DROP INDEX public."IX_RoleUser_UserId";
       public            dbuser    false    218            �           1259    16421    IX_Roles_Key    INDEX     J   CREATE UNIQUE INDEX "IX_Roles_Key" ON public."Roles" USING btree ("Key");
 "   DROP INDEX public."IX_Roles_Key";
       public            dbuser    false    219            �           1259    16422    IX_Users_Email    INDEX     N   CREATE UNIQUE INDEX "IX_Users_Email" ON public."Users" USING btree ("Email");
 $   DROP INDEX public."IX_Users_Email";
       public            dbuser    false    220            �           2606    16423 *   DeviceUser FK_DeviceUser_Devices_DevicesId    FK CONSTRAINT     �   ALTER TABLE ONLY public."DeviceUser"
    ADD CONSTRAINT "FK_DeviceUser_Devices_DevicesId" FOREIGN KEY ("DevicesId") REFERENCES public."Devices"("Id") ON DELETE CASCADE;
 X   ALTER TABLE ONLY public."DeviceUser" DROP CONSTRAINT "FK_DeviceUser_Devices_DevicesId";
       public          dbuser    false    216    3227    215            �           2606    16428 %   DeviceUser FK_DeviceUser_Users_UserId    FK CONSTRAINT     �   ALTER TABLE ONLY public."DeviceUser"
    ADD CONSTRAINT "FK_DeviceUser_Users_UserId" FOREIGN KEY ("UserId") REFERENCES public."Users"("Id") ON DELETE CASCADE;
 S   ALTER TABLE ONLY public."DeviceUser" DROP CONSTRAINT "FK_DeviceUser_Users_UserId";
       public          dbuser    false    3239    220    215            �           2606    16433 ,   Devices FK_Devices_MQTTUser_MQTTUserClientID    FK CONSTRAINT     �   ALTER TABLE ONLY public."Devices"
    ADD CONSTRAINT "FK_Devices_MQTTUser_MQTTUserClientID" FOREIGN KEY ("MQTTUserClientID") REFERENCES public."MQTTUser"("ClientID");
 Z   ALTER TABLE ONLY public."Devices" DROP CONSTRAINT "FK_Devices_MQTTUser_MQTTUserClientID";
       public          dbuser    false    216    217    3230            �           2606    16438 "   RoleUser FK_RoleUser_Roles_RolesId    FK CONSTRAINT     �   ALTER TABLE ONLY public."RoleUser"
    ADD CONSTRAINT "FK_RoleUser_Roles_RolesId" FOREIGN KEY ("RolesId") REFERENCES public."Roles"("Id") ON DELETE CASCADE;
 P   ALTER TABLE ONLY public."RoleUser" DROP CONSTRAINT "FK_RoleUser_Roles_RolesId";
       public          dbuser    false    218    3236    219            �           2606    16443 !   RoleUser FK_RoleUser_Users_UserId    FK CONSTRAINT     �   ALTER TABLE ONLY public."RoleUser"
    ADD CONSTRAINT "FK_RoleUser_Users_UserId" FOREIGN KEY ("UserId") REFERENCES public."Users"("Id") ON DELETE CASCADE;
 O   ALTER TABLE ONLY public."RoleUser" DROP CONSTRAINT "FK_RoleUser_Users_UserId";
       public          dbuser    false    3239    220    218            <   �   x�-Ȼ
�@@�>_1l�	d$��+��`���,Ġ߯��=w��6�0�Wx�辅�8��Ҳ�����}����*`=]�~�\z]+n�S�0Z]�W��y�-EMZ� �}D�РeKHI9]�M"V��,�Y�} L�(L      =   �   x�%���0D�|E�M!���WWFX4A�Rܗ�6!Ab������9�QM[]4R�>�����%�3F!Von�������g���q��J������<��D�i�YBe��>3�I{�{N6�������MW�K�-H�м����:j\�i.�Y�%� � ���4�      >   �   x���n�0 @����= ��-�,f�L�:��8�Z� ?�<��o�A�}�n2.����(:6��`�E&��[�	�C+�U~`M�W5_��j��n�n�j��c.�i6�;	G��Sd:60!�'[��r)-�d��ʱ̚i��$������������/v�]����%w�YI~0�U�������8�.���fS�7H����펜�J�_����b�jϊ����H�      ?   �   x��лj1��~�Bl�6x�.���T)\,|I/iF0����}y���S}�y�_�f���忞~�Ǹ;��xտ�,�}�e\��1�ki~�6���,�F��/5 &%�*,>[��ie�X8:*��رBU��P��]�qZ~�;�ֲf@�9���=et��&W�%B���S(�#�SU�F��Ƽ�̫�a�~�r�      @   �   x���K
�0 �}O1d�I�1MpU���V���3�PP�^{/�^j�� ����i��[�=y)�Ub�u��*�Z�Ʀ�\Ϟ\_���!U�1VB(�C��Vo@�?X�,�=�bL��#R�y��(���Jss�PΩ]�,���2T      A   "  x����n�@ ��OAؠ��@�Ӹ B/�x��\+�����+I�]����I�h��%�%s���1�/�J�J�JW���WԠ������9W,�r���������\b[����
`��,�a9�sF�@Z_�0+�Sާ�\�y)��
�_m��6�7R^�3=&:}��Ѝ�J
����%@�ߜS(�2� ˜�]�e���{�D���xM��:�6���!C���X�5���?-1��]s�p�&��!"z�����@�4�L%k9N�����&�Rlұyhn������ �dz&     