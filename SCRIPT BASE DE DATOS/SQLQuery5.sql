
EXEC dbo.GetDataByDateRange '2024-10-07', '2024-10-13';

CREATE PROCEDURE dbo.GetDataByDateRange
    @FechaInicio DATE,
    @FechaFin DATE
AS
BEGIN
    WITH DatosConFecha AS (
    SELECT 
			u.Nombre,
			d.Dia,
			d.Disponible,
			DATEADD(DAY, 
					CASE 
						WHEN d.Dia = 'Lunes' THEN 0
						WHEN d.Dia = 'Martes' THEN 1
						WHEN d.Dia = 'Miércoles' THEN 2
						WHEN d.Dia = 'Jueves' THEN 3
						WHEN d.Dia = 'Viernes' THEN 4
						WHEN d.Dia = 'Sábado' THEN 5
						WHEN d.Dia = 'Domingo' THEN 6
					END, s.FechaInicio) AS FechaDia
		FROM 
			Disponibilidad d
		JOIN 
			Agente a ON d.IdAgente = a.IdAgente AND a.Estado = 1
		JOIN 
			Usuario u ON a.IdUsuario = u.IdUsuario AND u.Estado = 1
		JOIN 
			Semanas s ON d.IdSemana = s.IdSemana
		WHERE 
			s.FechaInicio >= @FechaInicio AND s.FechaFin <= @FechaFin
	)
	SELECT 
		Nombre AS Usuario,
		SUM(CASE WHEN Dia = 'Lunes' THEN CAST(Disponible AS INT) END) AS [Lunes],
		MAX(CASE WHEN Dia = 'Martes' THEN CAST(Disponible AS INT) END) AS [Martes],
		MAX(CASE WHEN Dia = 'Miércoles' THEN CAST(Disponible AS INT) END) AS [Miércoles],
		MAX(CASE WHEN Dia = 'Jueves' THEN CAST(Disponible AS INT) END) AS [Jueves],
		MAX(CASE WHEN Dia = 'Viernes' THEN CAST(Disponible AS INT) END) AS [Viernes],
		MAX(CASE WHEN Dia = 'Sábado' THEN CAST(Disponible AS INT) END) AS [Sábado],
		MAX(CASE WHEN Dia = 'Domingo' THEN CAST(Disponible AS INT) END) AS [Domingo]
	FROM DatosConFecha
	GROUP BY Nombre
	ORDER BY Nombre;
END;


