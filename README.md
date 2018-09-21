<p><img src=https://github.com/AlfredoAbarca/ARTSP/blob/master/nuclear.png/></p>

# ARTSP

## Atomic Red Team Simple Parser

Desde ya hace algunos meses, comencé a trabajar con el framework de Att&ck liberado por el MITRE y me encontré con este el repositorio de [Atomic Red Team](https://github.com/redcanaryco/atomic-red-team) de Red Canary.

Este repositorio es un valioso compendio de las pruebas para valorar los controles de seguridad en equipos con Sistemas Operativos Windows, OS X y *NIX. 

De igual forma en este repositorio se encuentra un script en powershell para correr las pruebas en equipos windows y el compendio en una librería del resto de las pruebas en archivos con formato YAML. 

Sin embargo y a pesar de que están las pruebas muy bien organizadas, hace poco tuve la necesidad de correr la suite en sistemas OS X y la verdad se me hizo algo complicado, motivo por el cual desarrollé esta herramienta. 

El objetivo de la misma es muy sencillo, crear los scripts necesarios para correr en el sistema operativo evaluado, de forma simple y posteriormente solo modificar los parámetros necesarios correspondientes a nuestro escenario en particular. 

## ¿Cómo se usa? 
La herramienta es muy sencilla de utilizar, en este repositorio está el código fuente de la misma, por si requiere revisarlo o modificarlo. 
> Si no está familiarizado con el ambiente o lenguaje de programación .NET, puede simplemente descargar el archivo ejecutable que está en la carpeta con el nombre de BIN. 

Pasos a seguir: 

1.- Clonar o descargar el repositorio de [Atomic Red Team](https://github.com/redcanaryco/atomic-red-team) en su equipo (Windows).

2.- Compilar el proyecto o descargar el archivo ejecutable en su equipo, y luego simplemente ejecutarlo. 

Al hacerlo les deberá aparecer una ventana como la siguiente: 
<p><img src=https://github.com/AlfredoAbarca/ARTSP/blob/master/images/Image1.png/></p>

3.- Seleccionar los datos que se piden: 

**Importante:** 

La carpeta de origen (desde la cual leerá la información de las pruebas), corresponde a la que tiene el nombre de "***atomics***" dentro del repositorio de **Red Canary.**

4.- Seleccionar el directorio donde se almacenarán los scripts generados y qué tipo de scripts se generarán. 

5.- Dar click en el botón de generar

6.- Después de unos segundos, y si no ocurrió ningún error, deberá aparecer el siguiente mensaje: 
<p><img src=https://github.com/AlfredoAbarca/ARTSP/blob/master/images/Image2.png/></p>

7.- Vaya al directorio que eligió como destino y verá como la aplicación transformó las pruebas de este formato: 
<p><img src=https://github.com/AlfredoAbarca/ARTSP/blob/master/images/Image4.png/></p>

A este otro: 
<p><img src=https://github.com/AlfredoAbarca/ARTSP/blob/master/images/Image3.png/></p>

Mucho mas fácil y legible, además de concentrar todas las pruebas de una misma plataforma/lenguaje en un sólo archivo, listo para ser adecuado de acuerdo a sus necesidades particulares.  ;) 

Espero lo disfruten. 

## Problemas Conocidos

 - El código valida que los archivos YAML tengan como primer linea "---" para comenzar el procesamiento de la prueba. Si este patrón no se encuentra en el archivo, la función de procesamiento lo ignorará. El archivo de la prueba con el ID **T1033** no inicia con este patrón, por lo que para ser procesado, es necesario agregarlo manualmente o que el personal de Red Canary lo corrija ;). 
  
 - Este no es mi mejor código fuente por lo que seguramente existen algunas validaciones o bugs que puedan surgir, si es así, por favor háganmelo saber para mejorar esta aplicación. 

## Futuras Mejoras
Esta es la versión 1 de esta aplicación y como ya comenté surgió de la necesidad de simplificar la aplicación de las pruebas del framework de Att&ck, sin embargo considero que se pueden hacer otro tipo de mejoras y añadir funcionalidades, tales como: 

* Exportar toda la Base de Datos de pruebas a formato XML. 

* Pre-establecer los valores de los parámetros de entrada para customizar el ambiente de pruebas (previo a la generación de scripts). 

*  Descarga directa del repositorio de Github para evitar el proceso manual de la misma.  

Estas son algunas de las ideas que se me vienen a la mente en este momento, no se si las pueda hacer yo, pero pues debido a que el proyecto es Open Source, tengan la libertad de modificarlo, mencionando solo el proyecto original del cual tomaron el código inicial. 

Si tienen algún comentario adicional, detectan algún error o quieren aportar una sugerencia, pueden contactarme por este medio o por twitter.  **@aabarcab**

Que lo disfruten!!
