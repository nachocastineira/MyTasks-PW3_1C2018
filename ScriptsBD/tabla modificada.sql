use [PW3TP_20181C_Tareas]
ALTER TABLE [dbo].[Tarea]  WITH CHECK ADD  CONSTRAINT [FK_Tarea_Carpeta] FOREIGN KEY([IdCarpeta])
REFERENCES [dbo].[Carpeta] ([IdCarpeta])