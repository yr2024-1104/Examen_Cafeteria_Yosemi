# Examen_Cafeteria_Yosemi
# Nombre y matricula:
Yosemi Riveras (2024-1104)
# Descripción del programa
El programa es un sistema desarrollado en C# utilizando Windows Forms en Visual Studio. Su finalidad es facilitar el registro y cálculo de los pedidos realizados por los clientes de una cafetería.
# Datos de entrada
los datos de entrada son el Nombre del cliente, la fecha del pedido, la condición del cliente si es frecuente o no, el nombre del producto, el precio del producto, la cantidad de productos, el tamaño del producto si es normal o grande, selección de porción extra Sí o No y tipo de servicio (consumir en local o para llevar).
# Proceso
1. Recibir y validar los datos del cliente y los productos.
2. Calcular el precio unitario de cada producto, agregando RD$25 si es grande y RD$30 si tiene porción extra.
3. Multiplicar el precio unitario por la cantidad solicitada para obtener el total de cada producto.
4. Sumar los importes de todos los productos para calcular el subtotal.
5. Aplicar un descuento del 10 % sobre el subtotal cuando el cliente es frecuente.
6. Agregar RD$20 por concepto de empaque cuando el pedido es para llevar.
7. Calcular la base imponible restando el descuento y sumando el costo del empaque al subtotal.
8. Calcular el ITBIS correspondiente al 18 % de la base imponible.
9. Sumar la base imponible y el ITBIS para obtener el total a pagar.
10. Mostrar los resultados en la interfaz del programa.
# Datos de salida
La lista de productos agregados al pedido, la cantidad de cada productos, el precio unitario de cada producto, el total por cada producto, el subtotal de la compra, el descuento aplicado, el costo de empaque, el ITBIS del 18 % y el total general a pagar.
