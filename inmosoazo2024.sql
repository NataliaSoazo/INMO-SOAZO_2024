-- phpMyAdmin SQL Dump
-- version 5.2.1
-- https://www.phpmyadmin.net/
--
-- Servidor: 127.0.0.1
-- Tiempo de generación: 10-04-2024 a las 04:47:34
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
-- Base de datos: `inmosoazo2024`
--

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `contratos`
--

CREATE TABLE `contratos` (
  `Id` int(11) NOT NULL,
  `FechaInicio` date NOT NULL,
  `FechaTerm` date NOT NULL,
  `MontoMensual` double(15,0) NOT NULL,
  `IdInquilino` int(11) NOT NULL,
  `IdInmueble` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `contratos`
--

INSERT INTO `contratos` (`Id`, `FechaInicio`, `FechaTerm`, `MontoMensual`, `IdInquilino`, `IdInmueble`) VALUES
(3, '2024-04-16', '2024-04-16', 0, 4, 16),
(19, '2024-04-16', '2024-04-16', 0, 5, 15);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inmuebles`
--

CREATE TABLE `inmuebles` (
  `Id` int(11) NOT NULL,
  `Direccion` varchar(50) NOT NULL,
  `Ambientes` int(11) NOT NULL,
  `Uso` varchar(20) NOT NULL,
  `Valor` double(11,0) NOT NULL,
  `Disponible` varchar(4) NOT NULL,
  `Propietarioid` int(11) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inmuebles`
--

INSERT INTO `inmuebles` (`Id`, `Direccion`, `Ambientes`, `Uso`, `Valor`, `Disponible`, `Propietarioid`) VALUES
(15, 'Illia 900', 3, 'Vivienda', 80000, 'NO', 5),
(16, 'LAS HERAS 780', 2, 'DPTO', 180000, 'NO', 4),
(17, 'Av Lafinur 1200', 4, 'Comercia', 4000000, 'SI', 3),
(18, 'LAS HERAS 780', 3, 'DPTO', 1200000, 'LAS ', 3);

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `inquilinos`
--

CREATE TABLE `inquilinos` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(20) NOT NULL,
  `Apellido` varchar(20) NOT NULL,
  `Dni` varchar(12) NOT NULL,
  `Email` varchar(30) NOT NULL,
  `Telefono` varchar(20) NOT NULL,
  `Domicilio` varchar(20) NOT NULL,
  `Ciudad` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `inquilinos`
--

INSERT INTO `inquilinos` (`Id`, `Nombre`, `Apellido`, `Dni`, `Email`, `Telefono`, `Domicilio`, `Ciudad`) VALUES
(4, 'SELENA', 'GOMEZ', '15234765', 'celegomez@gmail.com', '2664578933', 'JUANA KOSLAY', 'JUANA KOSLAY'),
(5, 'MARCELA', 'SUAREZ', '35678465', 'msuarez@gmail.com', '2665347890', 'Sucre 290', 'SAN LUIS');

-- --------------------------------------------------------

--
-- Estructura de tabla para la tabla `propietarios`
--

CREATE TABLE `propietarios` (
  `Id` int(11) NOT NULL,
  `Nombre` varchar(20) NOT NULL,
  `Apellido` varchar(20) NOT NULL,
  `Dni` varchar(12) NOT NULL,
  `Email` varchar(30) NOT NULL,
  `Telefono` varchar(20) NOT NULL,
  `Domicilio` varchar(20) NOT NULL,
  `Ciudad` varchar(20) NOT NULL
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;

--
-- Volcado de datos para la tabla `propietarios`
--

INSERT INTO `propietarios` (`Id`, `Nombre`, `Apellido`, `Dni`, `Email`, `Telefono`, `Domicilio`, `Ciudad`) VALUES
(3, 'MANUEL', 'GONZALEZ', '27789234', 'mgonzalez@gmail.com', '2664235478', 'CHACO 89', 'SAN LUIS'),
(4, 'MARIELA', 'SOSA', '25678456', 'm.sosa@sanluis.gov.ar', '2665342345', 'B° CERROS COLORADOS', 'JUANA KOSLAY'),
(5, 'ADRIAN', 'SOSA BLANCO', '25678456', 'm.sosa@sanluis.gov.ar', '2664789825', 'B° CERROS COLORADOS', 'JUANA KOSLAY');

--
-- Índices para tablas volcadas
--

--
-- Indices de la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `IdInquilino` (`IdInquilino`),
  ADD KEY `IdInmueble` (`IdInmueble`);

--
-- Indices de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD PRIMARY KEY (`Id`),
  ADD KEY `Propietarioid` (`Propietarioid`);

--
-- Indices de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  ADD PRIMARY KEY (`Id`);

--
-- Indices de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  ADD PRIMARY KEY (`Id`);

--
-- AUTO_INCREMENT de las tablas volcadas
--

--
-- AUTO_INCREMENT de la tabla `contratos`
--
ALTER TABLE `contratos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=20;

--
-- AUTO_INCREMENT de la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=19;

--
-- AUTO_INCREMENT de la tabla `inquilinos`
--
ALTER TABLE `inquilinos`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=6;

--
-- AUTO_INCREMENT de la tabla `propietarios`
--
ALTER TABLE `propietarios`
  MODIFY `Id` int(11) NOT NULL AUTO_INCREMENT, AUTO_INCREMENT=17;

--
-- Restricciones para tablas volcadas
--

--
-- Filtros para la tabla `contratos`
--
ALTER TABLE `contratos`
  ADD CONSTRAINT `contratos_ibfk_1` FOREIGN KEY (`IdInquilino`) REFERENCES `inquilinos` (`Id`),
  ADD CONSTRAINT `contratos_ibfk_2` FOREIGN KEY (`IdInmueble`) REFERENCES `inmuebles` (`Id`);

--
-- Filtros para la tabla `inmuebles`
--
ALTER TABLE `inmuebles`
  ADD CONSTRAINT `inmuebles_ibfk_1` FOREIGN KEY (`Propietarioid`) REFERENCES `propietarios` (`id`);
COMMIT;

/*!40101 SET CHARACTER_SET_CLIENT=@OLD_CHARACTER_SET_CLIENT */;
/*!40101 SET CHARACTER_SET_RESULTS=@OLD_CHARACTER_SET_RESULTS */;
/*!40101 SET COLLATION_CONNECTION=@OLD_COLLATION_CONNECTION */;
