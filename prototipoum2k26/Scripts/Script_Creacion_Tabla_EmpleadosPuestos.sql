CREATE DATABASE IF NOT EXISTS umg_didactica;
USE umg_didactica;

CREATE TABLE IF NOT EXISTS tbl_Puestos (
    cmp_id_puesto INT AUTO_INCREMENT,
    cmp_nombre VARCHAR(100) NOT NULL,
    cmp_descripcion VARCHAR(200),
    cmp_salario_base DECIMAL(10,2) NOT NULL,
    CONSTRAINT pk_tbl_puestos PRIMARY KEY (cmp_id_puesto)
);
