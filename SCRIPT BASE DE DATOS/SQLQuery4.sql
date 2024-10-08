select * from Semanas
delete from Semanas where IdSemana IN (6,7)

INSERT INTO Semanas (FechaInicio, FechaFin, Numero) VALUES ('2024-10-07', '2024-10-13', 41), 
														   ('2024-10-14', '2024-10-20', 42);

select * from Agente

SELECT * FROM dbo.Semanas WHERE IdSemana = 41


INSERT INTO Disponibilidad (IdAgente, IdSemana, Dia, Disponible) 
VALUES 
(1, 4, 'Lunes', 0), (1, 4, 'Martes', 0), (1, 4, 'Mi�rcoles', 1), (1, 4, 'Jueves', 1), (1, 4, 'Viernes', 1), (1, 4, 'S�bado', 1), (1, 4, 'Domingo', 1),
(2, 4, 'Lunes', 1), (2, 4, 'Martes', 1), (2, 4, 'Mi�rcoles', 0), (2, 4, 'Jueves', 0), (2, 4, 'Viernes', 1), (2, 4, 'S�bado', 1), (2, 4, 'Domingo', 1),
(3, 4, 'Lunes', 1), (3, 4, 'Martes', 1), (3, 4, 'Mi�rcoles', 1), (3, 4, 'Jueves', 1), (3, 4, 'Viernes', 0), (3, 4, 'S�bado', 0), (3, 4, 'Domingo', 1);

-- Insertar Disponibilidad para usuarios en semana 2
INSERT INTO Disponibilidad (IdAgente, IdSemana, Dia, Disponible) 
VALUES 
(1, 5, 'Lunes', 1), (1, 5, 'Martes', 1), (1, 5, 'Mi�rcoles', 0), (1, 5, 'Jueves', 0), (1, 5, 'Viernes', 1), (1, 5, 'S�bado', 1), (1, 5, 'Domingo', 1),
(2, 5, 'Lunes', 1), (2, 5, 'Martes', 1), (2, 5, 'Mi�rcoles', 1), (2, 5, 'Jueves', 1), (2, 5, 'Viernes', 0), (2, 5, 'S�bado', 0), (2, 5, 'Domingo', 1),
(3, 5, 'Lunes', 1), (3, 5, 'Martes', 1), (3, 5, 'Mi�rcoles', 1), (3, 5, 'Jueves', 1), (3, 5, 'Viernes', 1), (3, 5, 'S�bado', 1), (3, 5, 'Domingo', 0);


----------------------------
DECLARE @FechaInicio DATE = '2024-10-07'; -- Fecha de inicio de la semana
DECLARE @FechaFin DATE = '2024-10-13';   -- Fecha de fin de la semana

