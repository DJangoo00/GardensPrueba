DROP DATABASE IF EXISTS jardineriadb;

CREATE DATABASE jardineriadb CHARACTER SET utf8mb4;

USE jardineriadb;

CREATE TABLE
    oficina (
        id VARCHAR(10) NOT NULL,
        ciudad VARCHAR(38) NOT NULL,
        pais VARCHAR(50) NOT NULL,
        region VARCHAR(50) DEFAULT NULL,
        Id_postal VARCHAR(10) NOT NULL,
        telefono VARCHAR(20) NOT NULL,
        linea_direccion1 VARCHAR(50) NOT NULL,
        linea_direccion2 VARCHAR(50) DEFAULT NULL,
        PRIMARY KEY (id)
    );

CREATE TABLE
    empleado (
        id INTEGER NOT NULL,
        nombre VARCHAR(50) NOT NULL,
        apellidol VARCHAR(50) NOT NULL,
        apellido2 VARCHAR(58) DEFAULT NULL,
        extension VARCHAR(10) NOT NULL,
        email VARCHAR(100) NOT NULL,
        id_oficina VARCHAR(10) NOT NULL,
        id_jefe INTEGER DEFAULT NULL,
        puesto VARCHAR(50) DEFAULT NULL,
        PRIMARY KEY (id),
        FOREIGN KEY (id_oficina) REFERENCES oficina (id),
        FOREIGN KEY (id_jefe) REFERENCES empleado (id)
    );

CREATE TABLE
    gama_producto (
        id VARCHAR(50) NOT NULL,
        descripcion_texto TEXT,
        descripcion_html TEXT,
        imagen VARCHAR(256),
        PRIMARY KEY (id)
    );

CREATE TABLE
    cliente (
        id INTEGER NOT NULL,
        nombre_cliente VARCHAR(58) NOT NULL,
        nombre_contacto VARCHAR(30) DEFAULT NULL,
        apellido_contacto VARCHAR(30) DEFAULT NULL,
        telefono VARCHAR(15) NOT NULL,
        fax VARCHAR(15) NOT NULL,
        linea_direccion1 VARCHAR(50) NOT NULL,
        linea_direccion2 VARCHAR(50) DEFAULT NULL,
        ciudad VARCHAR(50) NOT NULL,
        region VARCHAR(50) DEFAULT NULL,
        pais VARCHAR(50) DEFAULT NULL,
        Id_postal VARCHAR(10) DEFAULT NULL,
        id_empleado_rep_ventas INTEGER DEFAULT NULL,
        limite_credito NUMERIC(15, 2) DEFAULT NULL,
        PRIMARY KEY (id),
        FOREIGN KEY (id_empleado_rep_ventas) REFERENCES empleado (id)
    );

CREATE TABLE
    pedido (
        id INTEGER NOT NULL,
        fecha_pedido date NOT NULL,
        fecha_esperada date NOT NULL,
        fecha_entrega date DEFAULT NULL,
        estado VARCHAR(15) NOT NULL,
        comentarios TEXT,
        id_cliente INTEGER NOT NULL,
        PRIMARY KEY (id),
        FOREIGN KEY (id_cliente) REFERENCES cliente (id)
    );

CREATE TABLE
    producto (
        id VARCHAR(15) NOT NULL,
        nombre VARCHAR(70) NOT NULL,
        gama VARCHAR(50) NOT NULL,
        dimensiones VARCHAR(25) NULL,
        proveedor VARCHAR(50) DEFAULT NULL,
        descripcion text NULL,
        cantidad_en_stock SMALLINT NOT NULL,
        precio_venta NUMERIC(15, 2) NOT NULL,
        precio_proveedor NUMERIC(15, 2) DEFAULT NULL,
        PRIMARY KEY (id),
        FOREIGN KEY (gama) REFERENCES gama_producto (id)
    );

CREATE TABLE
    detalle_pedido (
        id_pedido INTEGER NOT NULL,
        id_producto VARCHAR(15) NOT NULL,
        cantidad INTEGER NOT NULL,
        precio_unidad NUMERIC(15, 2) NOT NULL,
        numero_linea SMALLINT NOT NULL,
        PRIMARY KEY (id_pedido, id_producto),
        FOREIGN KEY (id_pedido) REFERENCES pedido (id),
        FOREIGN KEY (id_producto) REFERENCES producto (id)
    );

CREATE TABLE
    pago (
        id_cliente INTEGER NOT NULL,
        forma_pago VARCHAR(48) NOT NULL,
        id_transaccion VARCHAR(50) NOT NULL,
        fecha_pago date NOT NULL,
        total NUMERIC(15, 2) NOT NULL,
        PRIMARY KEY (id_cliente, id_transaccion),
        FOREIGN KEY (id_cliente) REFERENCES cliente (id)
    );

-- veterinariadb.`role` definition
CREATE TABLE
    `role` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `roleName` varchar(50) NOT NULL,
        PRIMARY KEY (`Id`)
    );

-- veterinariadb.`user` definition
CREATE TABLE
    `user` (
        `Id` int NOT NULL AUTO_INCREMENT,
        `Nombre` varchar(50) NOT NULL,
        `Correo` varchar(100) NOT NULL,
        `Password` varchar(255) NOT NULL,
        PRIMARY KEY (`Id`)
    );

-- veterinariadb.userrole definition
CREATE TABLE
    `userrole` (
        `IdUserFk` int NOT NULL,
        `IdRoleFk` int NOT NULL,
        PRIMARY KEY (`IdRoleFk`, `IdUserFk`),
        KEY `IX_userrole_IdUserFk` (`IdUserFk`),
        CONSTRAINT `FK_userrole_role_IdRoleFk` FOREIGN KEY (`IdRoleFk`) REFERENCES `role` (`Id`) ON DELETE CASCADE,
        CONSTRAINT `FK_userrole_user_IdUserFk` FOREIGN KEY (`IdUserFk`) REFERENCES `user` (`Id`) ON DELETE CASCADE
    );