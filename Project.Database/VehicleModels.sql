CREATE TABLE [dbo].[VehicleModels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[MakeId] [int] NOT NULL,
	[Name] [nvarchar](50) NOT NULL,
	[Abrv] [nvarchar](20) NOT NULL,
 CONSTRAINT [PK_VehicleModels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

ALTER TABLE [dbo].[VehicleModels] ADD  CONSTRAINT [FK_VehicleModels_VehicleMakes_MakeId] FOREIGN KEY([MakeId])
REFERENCES [dbo].[VehicleMakes] ([Id])
ON DELETE CASCADE
GO

ALTER TABLE [dbo].[VehicleModels] CHECK CONSTRAINT [FK_VehicleModels_VehicleMakes_MakeId]
GO


