-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 26-09-2025 a las 07:16:53
-- Versión del servidor: 10.4.32-MariaDB
-- Versión de PHP: 8.2.12

SET SQL_MODE = "NO_AUTO_VALUE_ON_ZERO";
START TRANSACTION;
SET time_zone = "+00:00";


/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8mb4 */;

--
-- Base de datos: `inmobiliaria-lucasosella`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contrato`
--

CREATE TABLE `contrato` (
  `id` int(11) NOT NULL,
  `id_inquilino` int(11) NOT NULL,
  `id_inmueble` int(11) NOT NULL,
  `fecha_inicio` date NOT NULL,
  `fecha_fin` date NOT NULL,
  `monto_mensual` decimal(10,0) NOT NULL,
  `id_usuario_creador` int(11) NOT NULL,
  `id_usuario_finalizador` int(11) DEFAULT NULL,
  `fecha_rescision` date DEFAULT NULL,
  `multa_pagada` tinyint(1) DEFAULT NULL,
  `multa` double DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contrato`
--

INSERT INTO `contrato` (`id`, `id_inquilino`, `id_inmueble`, `fecha_inicio`, `fecha_fin`, `monto_mensual`, `id_usuario_creador`, `id_usuario_finalizador`, `fecha_rescision`, `multa_pagada`, `multa`) VALUES
(2, 1, 1, '2025-09-13', '2025-12-13', 12000, 10, 10, '2025-09-26', 1, 36000),
(8, 1, 2, '2025-09-25', '2025-11-26', 1, 10, NULL, '2025-09-26', 1, 3),
(12, 3, 3, '2025-09-26', '2025-12-26', 12000, 10, 10, '2025-09-26', 1, 72000);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmueble`
--

CREATE TABLE `inmueble` (
  `id` int(11) NOT NULL,
  `direccion` varchar(100) NOT NULL,
  `uso` enum('RESIDENCIAL','COMERCIAL','','') NOT NULL,
  `ambientes` int(11) NOT NULL,
  `coordenadas` varchar(100) NOT NULL,
  `precio` decimal(10,0) NOT NULL,
  `estado` enum('DISPONIBLE','SUSPENDIDO','OCUPADO','') NOT NULL,
  `id_propietario` int(11) NOT NULL,
  `id_tipo` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmueble`
--

INSERT INTO `inmueble` (`id`, `direccion`, `uso`, `ambientes`, `coordenadas`, `precio`, `estado`, `id_propietario`, `id_tipo`) VALUES
(1, '20 de junio', 'RESIDENCIAL', 2, '-34.6037, -58.3816', 150000, 'OCUPADO', 2, 1),
(2, 'calle falsa', 'COMERCIAL', 1, '-77.000, 50.600', 15000, 'OCUPADO', 2, 1),
(3, 'Calle de pepito', 'RESIDENCIAL', 4, '-50.700, -32.500', 20000, 'OCUPADO', 10, 2),
(4, 'Caller Cordoba ', 'RESIDENCIAL', 4, '-75.4216, -148.0389', 50000, 'DISPONIBLE', 2, 2);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilino`
--

CREATE TABLE `inquilino` (
  `id` int(11) NOT NULL,
  `dni` varchar(20) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `telefono` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `direccion` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilino`
--

INSERT INTO `inquilino` (`id`, `dni`, `nombre`, `telefono`, `email`, `direccion`) VALUES
(1, '50355831', 'Inquilino', '255666444', 'In@gmail.com', 'Calle Inquilino'),
(3, '13910509', 'Walter Osella', '2664322331', 'wo@gmail.com', '20 de junio'),
(4, '321', 'testInquilino', '2222222222', 'testInq@gmail.com', 'testDirecInquilino'),
(5, '43282117', 'Lucas Agustin Osella Rocha', '2664506790', 'lucasosella01@gmail.com', '20 de junio');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `pago`
--

CREATE TABLE `pago` (
  `id` int(11) NOT NULL,
  `id_contrato` int(11) NOT NULL,
  `numero_pago` int(11) NOT NULL,
  `fecha_pago` date NOT NULL,
  `detalle` varchar(100) NOT NULL,
  `importe` decimal(10,0) NOT NULL,
  `estado` enum('ACTIVO','ANULADO','PENDIENTE') NOT NULL,
  `id_usuario_creador` int(11) NOT NULL,
  `id_usuario_finalizador` int(11) DEFAULT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `pago`
--

INSERT INTO `pago` (`id`, `id_contrato`, `numero_pago`, `fecha_pago`, `detalle`, `importe`, `estado`, `id_usuario_creador`, `id_usuario_finalizador`) VALUES
(3, 8, 1, '2025-09-25', 'pago de alquiler', 15000, 'PENDIENTE', 10, 10),
(4, 12, 1, '2025-09-26', 'Pago de alquiler', 12000, 'PENDIENTE', 10, 10),
(7, 12, 1, '2025-09-26', 'Multa por rescisión anticipada', 24000, 'ANULADO', 3, 3),
(19, 12, 1, '2025-09-26', 'Multa por rescisión anticipada', 72000, 'ACTIVO', 3, 3),
(20, 8, 1, '2025-09-26', 'Multa por rescisión anticipada', 3, 'ACTIVO', 3, 3),
(21, 2, 1, '2025-09-26', 'Multa por rescisión anticipada', 36000, 'ANULADO', 3, 3);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietario`
--

CREATE TABLE `propietario` (
  `id` int(11) NOT NULL,
  `dni` int(20) NOT NULL,
  `apellido` varchar(100) NOT NULL,
  `nombre` varchar(100) NOT NULL,
  `telefono` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `direccion` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietario`
--

INSERT INTO `propietario` (`id`, `dni`, `apellido`, `nombre`, `telefono`, `email`, `direccion`) VALUES
(2, 43282117, 'Osella', 'Lucas Agustin', '2664506790', 'lucasosella01@gmail.com', '20 de junio'),
(3, 44019537, 'Arce', 'Juan El Diego', '2664758595', 'jd@gmail.com', 'Calle 12 1032'),
(4, 44787969, 'propietarioApellido', 'propietarioNombre', '2657421678', 'propietario1@gmail.com', 'propietarioCasa'),
(7, 45588595, 'testapellidopro', 'testnombrepropietario', '266555444888', 'testInq@gmail.com', 'testDirecInquilino2'),
(10, 43282118, 'nuñes', 'pepito', '02222222222', 'email@gmail.com', 'casa falsa');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipo_inmueble`
--

CREATE TABLE `tipo_inmueble` (
  `id` int(11) NOT NULL,
  `nombre` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tipo_inmueble`
--

INSERT INTO `tipo_inmueble` (`id`, `nombre`) VALUES
(1, 'Departamento'),
(2, 'Casa'),
(4, 'Local comercial');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `tipo_usuario`
--

CREATE TABLE `tipo_usuario` (
  `id_tipo_usuario` int(11) NOT NULL,
  `rol_usuario` varchar(100) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `tipo_usuario`
--

INSERT INTO `tipo_usuario` (`id_tipo_usuario`, `rol_usuario`) VALUES
(1, 'ADMIN'),
(2, 'EMPLEADO');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `usuario`
--

CREATE TABLE `usuario` (
  `id` int(11) NOT NULL,
  `nombre_usuario` varchar(100) NOT NULL,
  `apellido_usuario` varchar(100) NOT NULL,
  `email` varchar(100) NOT NULL,
  `password` varchar(255) NOT NULL,
  `id_tipo_usuario` int(11) NOT NULL,
  `activo` tinyint(4) NOT NULL,
  `creado` date NOT NULL,
  `foto` varchar(255) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `usuario`
--

INSERT INTO `usuario` (`id`, `nombre_usuario`, `apellido_usuario`, `email`, `password`, `id_tipo_usuario`, `activo`, `creado`, `foto`) VALUES
(3, 'Lucas', 'Osella', 'lo@gmail.com', 'AQAAAAIAAYagAAAAEOaJSHUq97GlRrEnuJCRPB/F3xayvucNVlIe7Dm3k2AAOI20ccEGqOkv2nr0GGwMYA==', 1, 0, '2025-09-05', ''),
(10, 'Lucas Agustin', 'Osella Rocha', 'lucasosella01@gmail.com', 'AQAAAAIAAYagAAAAEKYRYcNAJzqVx0pxFTz3yfs2hNueaqkoqkXNqccA9PYVIVdJ0xUSa5RZsNRoEwSLDg==', 1, 1, '0000-00-00', '/images/usuarios/10.jpg'),
(12, 'Juan', 'Diego Arce', 'jd@gmail.com', 'AQAAAAIAAYagAAAAEN/T5NheBmzM1B/kPAVclPYtOcwmZaqUds9/hc+NSyO+z+XfdYsh+EHo3KbBplPA3A==', 2, 1, '0000-00-00', '/images/usuarios/12.jpg'),
(13, 'pepito el man', 'rial', 'pr@gmail.com', 'AQAAAAIAAYagAAAAEGkWq3yU/x1pAlPm/IKmc5el+oiKPqclCM/cJiBKQplT/SWUt73ypVl+XuN5qfuy4g==', 2, 0, '0000-00-00', ''),
(14, 'pepe el pepito', 'rial', 'pr@gmail.com', 'AQAAAAIAAYagAAAAEPBxTmjcJKNQgC0lSD8ly2cqo4bkWbOQxBhRkCsWc1q7ZoLZo8ti+i8s+AToeeqe3A==', 1, 1, '0000-00-00', ''),
(18, 'El usuario', 'uno', 'elusurio1@gmail.com', 'AQAAAAIAAYagAAAAELd8+KAbXjWz2KdpaN9RvQ1Gm1ttb22O/Zo+7Um7hSV3zcvRffHghZjR12svTyY1CA==', 2, 1, '0000-00-00', '/images/usuarios/18.jpg'),
(19, 'El usuario', 'dos', 'elusurio2@gmail.com', 'AQAAAAIAAYagAAAAECkZujVnk9jscYixBnrO4Tew8Za9JY7zXTYvWlTqhyjUb+SvbIgvTjyMLvWjjiwxHw==', 2, 1, '0000-00-00', '/images/usuarios/19.jpg'),
(20, 'el usuario', 'tres', 'elusuario3@gmail.com', 'AQAAAAIAAYagAAAAEH7bRUdBCxl2civGJ34KM3Dl4OwCeCFBzoyFfAKvZi9wQD3HverGlW5+Cmywq6P+Mw==', 2, 1, '0000-00-00', '/images/usuarios/20.jpg'),
(21, 'El usuario ', 'Cuatro', 'usuariocuatro@gmail.com', 'AQAAAAIAAYagAAAAEFlW22vRpPL3RptLH3BdDVDy3n+IvcROfVRdv8uuZHJgfuWxwZ2oIFeT+Oh50CCoHA==', 2, 0, '0000-00-00', '/images/usuarios/21.jpg');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_inmueble` (`id_inmueble`),
  ADD KEY `id_inquilino` (`id_inquilino`),
  ADD KEY `id_usuario_creador` (`id_usuario_creador`),
  ADD KEY `id_usuario_finalizador` (`id_usuario_finalizador`);

--
-- Indices de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_propietario` (`id_propietario`),
  ADD KEY `id_tipo` (`id_tipo`);

--
-- Indices de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  ADD PRIMARY KEY (`id`,`dni`);

--
-- Indices de la tabla `pago`
--
ALTER TABLE `pago`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_contrato` (`id_contrato`),
  ADD KEY `id_usuario_creador` (`id_usuario_creador`),
  ADD KEY `id_usuario_finalizador` (`id_usuario_finalizador`);

--
-- Indices de la tabla `propietario`
--
ALTER TABLE `propietario`
  ADD PRIMARY KEY (`id`,`dni`);

--
-- Indices de la tabla `tipo_inmueble`
--
ALTER TABLE `tipo_inmueble`
  ADD PRIMARY KEY (`id`);

--
-- Indices de la tabla `tipo_usuario`
--
ALTER TABLE `tipo_usuario`
  ADD PRIMARY KEY (`id_tipo_usuario`);

--
-- Indices de la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD PRIMARY KEY (`id`),
  ADD KEY `id_tipo_usuario` (`id_tipo_usuario`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contrato`
--
ALTER TABLE `contrato`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=13;

--
-- AUTO_INCREMENT de la tabla `inmueble`
--
ALTER TABLE `inmueble`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `inquilino`
--
ALTER TABLE `inquilino`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `pago`
--
ALTER TABLE `pago`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- AUTO_INCREMENT de la tabla `propietario`
--
ALTER TABLE `propietario`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=12;

--
-- AUTO_INCREMENT de la tabla `tipo_inmueble`
--
ALTER TABLE `tipo_inmueble`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=5;

--
-- AUTO_INCREMENT de la tabla `tipo_usuario`
--
ALTER TABLE `tipo_usuario`
  MODIFY `id_tipo_usuario` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=3;

--
-- AUTO_INCREMENT de la tabla `usuario`
--
ALTER TABLE `usuario`
  MODIFY `id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=22;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contrato`
--
ALTER TABLE `contrato`
  ADD CONSTRAINT `contrato_ibfk_1` FOREIGN KEY (`id_inmueble`) REFERENCES `inmueble` (`id`),
  ADD CONSTRAINT `contrato_ibfk_2` FOREIGN KEY (`id_inquilino`) REFERENCES `inquilino` (`id`),
  ADD CONSTRAINT `contrato_ibfk_3` FOREIGN KEY (`id_usuario_creador`) REFERENCES `usuario` (`id`),
  ADD CONSTRAINT `contrato_ibfk_4` FOREIGN KEY (`id_usuario_finalizador`) REFERENCES `usuario` (`id`);

--
-- Filtros para la tabla `inmueble`
--
ALTER TABLE `inmueble`
  ADD CONSTRAINT `inmueble_ibfk_1` FOREIGN KEY (`id_propietario`) REFERENCES `propietario` (`id`),
  ADD CONSTRAINT `inmueble_ibfk_2` FOREIGN KEY (`id_tipo`) REFERENCES `tipo_inmueble` (`id`);

--
-- Filtros para la tabla `pago`
--
ALTER TABLE `pago`
  ADD CONSTRAINT `pago_ibfk_1` FOREIGN KEY (`id_contrato`) REFERENCES `contrato` (`id`),
  ADD CONSTRAINT `pago_ibfk_2` FOREIGN KEY (`id_usuario_creador`) REFERENCES `usuario` (`id`),
  ADD CONSTRAINT `pago_ibfk_3` FOREIGN KEY (`id_usuario_finalizador`) REFERENCES `usuario` (`id`);

--
-- Filtros para la tabla `usuario`
--
ALTER TABLE `usuario`
  ADD CONSTRAINT `usuario_ibfk_1` FOREIGN KEY (`id_tipo_usuario`) REFERENCES `tipo_usuario` (`id_tipo_usuario`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
