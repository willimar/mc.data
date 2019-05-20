-- USE `mcdata`;
-- MySQL dump 10.13  Distrib 5.5.16, for Win32 (x86)
--
-- Host: localhost    Database: cep2012
-- ------------------------------------------------------
-- Server version	5.5.25

/*!40101 SET @OLD_CHARACTER_SET_CLIENT=@@CHARACTER_SET_CLIENT */;
/*!40101 SET @OLD_CHARACTER_SET_RESULTS=@@CHARACTER_SET_RESULTS */;
/*!40101 SET @OLD_COLLATION_CONNECTION=@@COLLATION_CONNECTION */;
/*!40101 SET NAMES utf8 */;
/*!40014 SET @OLD_UNIQUE_CHECKS=@@UNIQUE_CHECKS, UNIQUE_CHECKS=0 */;
/*!40014 SET @OLD_FOREIGN_KEY_CHECKS=@@FOREIGN_KEY_CHECKS, FOREIGN_KEY_CHECKS=0 */;
/*!40101 SET @OLD_SQL_MODE=@@SQL_MODE, SQL_MODE='NO_AUTO_VALUE_ON_ZERO' */;
/*!40111 SET @OLD_SQL_NOTES=@@SQL_NOTES, SQL_NOTES=0 */;

--
-- Temporary table structure for view `endereco_completo`
--

DROP TABLE IF EXISTS `endereco_completo`;
/*!50001 DROP VIEW IF EXISTS `endereco_completo`*/;
SET @saved_cs_client     = @@character_set_client;
SET character_set_client = utf8;
/*!50001 CREATE TABLE `endereco_completo` (
  `logradouro` varchar(300),
  `endereco` varchar(300),
  `bairro` varchar(200),
  `cidade` varchar(200),
  `uf` varchar(2),
  `cep` varchar(9)
) ENGINE=MyISAM */;
SET character_set_client = @saved_cs_client;

--
-- Table structure for table `tend_estado`
--

DROP TABLE IF EXISTS `tend_estado`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tend_estado` (
  `id_estado` int(11) NOT NULL,
  `estado` varchar(150) NOT NULL,
  `uf` varchar(2) NOT NULL,
  PRIMARY KEY (`id_estado`)
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

--
-- Dumping data for table `tend_estado`
--

INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (1,'Acre','AC');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (2,'Alagoas','AL');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (3,'Amazonas','AM');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (4,'Amapá','AP');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (5,'Bahia','BA');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (6,'Ceará','CE');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (7,'Brasília','DF');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (8,'Espírito Santo','ES');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (9,'Goiás','GO');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (10,'Maranhão','MA');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (11,'Minas Gerais','MG');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (12,'Mato Grosso do Sul','MS');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (13,'Mato Grosso','MT');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (14,'Pará','PA');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (15,'Paraíba','PB');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (16,'Pernambuco','PE');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (17,'Piauí','PI');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (18,'Paraná','PR');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (19,'Rio de Janeiro','RJ');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (20,'Rio Grande do Norte','RN');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (21,'Rondônia','RO');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (22,'Roraima','RR');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (23,'Rio Grande do Sul','RS');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (24,'Santa Catarina','SC');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (25,'Sergipe','SE');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (26,'São Paulo','SP');
INSERT INTO `tend_estado` (`id_estado`, `estado`, `uf`) VALUES (27,'Tocantins','TO');

--
-- Table structure for table `tend_endereco`
--

DROP TABLE IF EXISTS `tend_endereco`;
/*!40101 SET @saved_cs_client     = @@character_set_client */;
/*!40101 SET character_set_client = utf8 */;
CREATE TABLE `tend_endereco` (
  `cep` varchar(9) NOT NULL,
  `id_cidade` int(11) NOT NULL,
  `id_bairro` int(11) NOT NULL,
  `logradouro` varchar(300) NOT NULL,
  `endereco` varchar(300) NOT NULL,
  `endereco_completo` varchar(300) NOT NULL,
  PRIMARY KEY (`cep`),
  KEY `endereco_cidade_idx` (`id_cidade`),
  KEY `endereco_bairro_idx` (`id_bairro`),
  CONSTRAINT `endereco_bairro` FOREIGN KEY (`id_bairro`) REFERENCES `tend_bairro` (`id_bairro`) ON DELETE NO ACTION ON UPDATE NO ACTION,
  CONSTRAINT `endereco_cidade` FOREIGN KEY (`id_cidade`) REFERENCES `tend_cidade` (`id_cidade`) ON DELETE NO ACTION ON UPDATE NO ACTION
) ENGINE=InnoDB DEFAULT CHARSET=latin1;
/*!40101 SET character_set_client = @saved_cs_client */;

