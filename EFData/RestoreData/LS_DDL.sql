USE [LightSystem]
GO
/****** Object:  Table [dbo].[BaseEffects]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[BaseEffects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Category] [nvarchar](max) NOT NULL,
	[Params] [nvarchar](max) NOT NULL,
	[PointType] [int] NOT NULL,
	[Remark] [nvarchar](max) NULL,
 CONSTRAINT [PK_BaseEffects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ControlChannels]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ControlChannels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ChannelNo] [int] NOT NULL,
	[HaveDimmer] [bit] NULL,
	[ControlDevice_Id] [int] NOT NULL,
 CONSTRAINT [PK_ControlChannels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ControlChannels_ArtNetControlChannel]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[ControlChannels_ArtNetControlChannel](
	[IPAddress] [nvarchar](max) NOT NULL,
	[PortNo] [int] NOT NULL,
	[LS_Assignments] [varbinary](max) NULL,
	[Id] [int] NOT NULL,
 CONSTRAINT [PK_ControlChannels_ArtNetControlChannel] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[ControlDevices]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ControlDevices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[CanDimming] [bit] NOT NULL,
	[Model] [nvarchar](max) NOT NULL,
	[Profile] [nvarchar](max) NULL,
	[Remark] [nvarchar](max) NULL,
	[ControlSpace_Id] [int] NOT NULL,
 CONSTRAINT [PK_ControlDevices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[ControlSpaces]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ControlSpaces](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ControlChCount] [int] NOT NULL,
	[EventChCount] [int] NOT NULL,
 CONSTRAINT [PK_ControlSpaces] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Effects]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Effects](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Category] [nvarchar](max) NOT NULL,
	[Params] [nvarchar](max) NOT NULL,
	[PointType] [int] NOT NULL,
	[Type] [nvarchar](max) NOT NULL,
	[Remark] [nvarchar](max) NULL,
	[Pattern_Id] [int] NULL,
 CONSTRAINT [PK_Effects] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EnvironmentItems]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EnvironmentItems](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Model] [nvarchar](max) NOT NULL,
	[DeviceType] [int] NOT NULL,
	[Profile] [nvarchar](max) NOT NULL,
	[ControlSpace_Id] [int] NOT NULL,
 CONSTRAINT [PK_EnvironmentItems] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventChannelLightZone]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventChannelLightZone](
	[EventChannel_Id] [int] NOT NULL,
	[LightZones_Id] [int] NOT NULL,
 CONSTRAINT [PK_EventChannelLightZone] PRIMARY KEY CLUSTERED 
(
	[EventChannel_Id] ASC,
	[LightZones_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventChannels]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventChannels](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[ChannelNo] [int] NOT NULL,
	[EventName] [nvarchar](max) NOT NULL,
	[EventDevice_Id] [int] NOT NULL,
	[Event_Id] [int] NOT NULL,
 CONSTRAINT [PK_EventChannels] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[EventDevices]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EventDevices](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Mode] [int] NOT NULL,
	[Model] [nvarchar](max) NOT NULL,
	[Profile] [nvarchar](max) NULL,
	[Remark] [nvarchar](max) NULL,
	[ControlSpace_Id] [int] NOT NULL,
	[Partition_Id] [int] NOT NULL,
 CONSTRAINT [PK_EventDevices] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Events]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Events](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Events] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[GammaLightElement]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[GammaLightElement](
	[Gammas_Id] [int] NOT NULL,
	[LightElements_Id] [int] NOT NULL,
 CONSTRAINT [PK_GammaLightElement] PRIMARY KEY CLUSTERED 
(
	[Gammas_Id] ASC,
	[LightElements_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Gammas]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
SET ANSI_PADDING ON
GO
CREATE TABLE [dbo].[Gammas](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Value] [varbinary](max) NOT NULL,
 CONSTRAINT [PK_Gammas] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
SET ANSI_PADDING OFF
GO
/****** Object:  Table [dbo].[LE_Types]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LE_Types](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NULL,
	[Remark] [nvarchar](max) NULL,
	[PointType] [int] NOT NULL,
	[IsActive] [bit] NULL,
	[ControlSpace_Id] [int] NOT NULL,
 CONSTRAINT [PK_LE_Types] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LightElementLightZone]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LightElementLightZone](
	[LightElements_Id] [int] NOT NULL,
	[LightZones_Id] [int] NOT NULL,
 CONSTRAINT [PK_LightElementLightZone] PRIMARY KEY CLUSTERED 
(
	[LightElements_Id] ASC,
	[LightZones_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LightElements]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LightElements](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[StartPoint] [int] NOT NULL,
	[CanDimming] [bit] NOT NULL,
	[Remark] [nvarchar](max) NULL,
	[PointType] [int] NULL,
	[ControlChannel_Id] [int] NULL,
	[Partition_Id] [int] NOT NULL,
	[ControlSpace_Id] [int] NOT NULL,
 CONSTRAINT [PK_LightElements] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LightElements_LightStrip]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LightElements_LightStrip](
	[PointCount] [int] NOT NULL,
	[Direction] [int] NOT NULL,
	[ColorSequence] [nvarchar](max) NOT NULL,
	[Id] [int] NOT NULL,
 CONSTRAINT [PK_LightElements_LightStrip] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LightZoneEffect]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LightZoneEffect](
	[LightZones_Id] [int] NOT NULL,
	[Effects_Id] [int] NOT NULL,
 CONSTRAINT [PK_LightZoneEffect] PRIMARY KEY CLUSTERED 
(
	[LightZones_Id] ASC,
	[Effects_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[LightZones]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LightZones](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Remark] [nvarchar](max) NULL,
	[Partition_Id] [int] NOT NULL,
 CONSTRAINT [PK_LightZones] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Partitions]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Partitions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Partitions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Patterns]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Patterns](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Value] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Patterns] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
/****** Object:  Table [dbo].[SceneLightZone]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[SceneLightZone](
	[Scenes_Id] [int] NOT NULL,
	[LightZones_Id] [int] NOT NULL,
 CONSTRAINT [PK_SceneLightZone] PRIMARY KEY CLUSTERED 
(
	[Scenes_Id] ASC,
	[LightZones_Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO
/****** Object:  Table [dbo].[Scenes]    Script Date: 26.03.2016 12:19:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Scenes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](max) NOT NULL,
	[Remark] [nvarchar](max) NULL,
	[Partition_Id] [int] NOT NULL,
 CONSTRAINT [PK_Scenes] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO
ALTER TABLE [dbo].[ControlChannels]  WITH CHECK ADD  CONSTRAINT [FK_PowerDeviceLightChannel] FOREIGN KEY([ControlDevice_Id])
REFERENCES [dbo].[ControlDevices] ([Id])
GO
ALTER TABLE [dbo].[ControlChannels] CHECK CONSTRAINT [FK_PowerDeviceLightChannel]
GO
ALTER TABLE [dbo].[ControlChannels_ArtNetControlChannel]  WITH CHECK ADD  CONSTRAINT [FK_ArtNetControlChannel_inherits_ControlChannel] FOREIGN KEY([Id])
REFERENCES [dbo].[ControlChannels] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[ControlChannels_ArtNetControlChannel] CHECK CONSTRAINT [FK_ArtNetControlChannel_inherits_ControlChannel]
GO
ALTER TABLE [dbo].[ControlDevices]  WITH CHECK ADD  CONSTRAINT [FK_ControlSpacePowerDevice] FOREIGN KEY([ControlSpace_Id])
REFERENCES [dbo].[ControlSpaces] ([Id])
GO
ALTER TABLE [dbo].[ControlDevices] CHECK CONSTRAINT [FK_ControlSpacePowerDevice]
GO
ALTER TABLE [dbo].[Effects]  WITH CHECK ADD  CONSTRAINT [FK_PatternEffect] FOREIGN KEY([Pattern_Id])
REFERENCES [dbo].[Patterns] ([Id])
GO
ALTER TABLE [dbo].[Effects] CHECK CONSTRAINT [FK_PatternEffect]
GO
ALTER TABLE [dbo].[EnvironmentItems]  WITH CHECK ADD  CONSTRAINT [FK_ControlSpaceEnvironmentItem] FOREIGN KEY([ControlSpace_Id])
REFERENCES [dbo].[ControlSpaces] ([Id])
GO
ALTER TABLE [dbo].[EnvironmentItems] CHECK CONSTRAINT [FK_ControlSpaceEnvironmentItem]
GO
ALTER TABLE [dbo].[EventChannelLightZone]  WITH CHECK ADD  CONSTRAINT [FK_EventChannelLightZone_EventChannel] FOREIGN KEY([EventChannel_Id])
REFERENCES [dbo].[EventChannels] ([Id])
GO
ALTER TABLE [dbo].[EventChannelLightZone] CHECK CONSTRAINT [FK_EventChannelLightZone_EventChannel]
GO
ALTER TABLE [dbo].[EventChannelLightZone]  WITH CHECK ADD  CONSTRAINT [FK_EventChannelLightZone_LightZone] FOREIGN KEY([LightZones_Id])
REFERENCES [dbo].[LightZones] ([Id])
GO
ALTER TABLE [dbo].[EventChannelLightZone] CHECK CONSTRAINT [FK_EventChannelLightZone_LightZone]
GO
ALTER TABLE [dbo].[EventChannels]  WITH CHECK ADD  CONSTRAINT [FK_EventDeviceEventChannel] FOREIGN KEY([EventDevice_Id])
REFERENCES [dbo].[EventDevices] ([Id])
GO
ALTER TABLE [dbo].[EventChannels] CHECK CONSTRAINT [FK_EventDeviceEventChannel]
GO
ALTER TABLE [dbo].[EventChannels]  WITH CHECK ADD  CONSTRAINT [FK_EventEventChannel] FOREIGN KEY([Event_Id])
REFERENCES [dbo].[Events] ([Id])
GO
ALTER TABLE [dbo].[EventChannels] CHECK CONSTRAINT [FK_EventEventChannel]
GO
ALTER TABLE [dbo].[EventDevices]  WITH CHECK ADD  CONSTRAINT [FK_ControlSpaceEventDevice] FOREIGN KEY([ControlSpace_Id])
REFERENCES [dbo].[ControlSpaces] ([Id])
GO
ALTER TABLE [dbo].[EventDevices] CHECK CONSTRAINT [FK_ControlSpaceEventDevice]
GO
ALTER TABLE [dbo].[EventDevices]  WITH CHECK ADD  CONSTRAINT [FK_PartitionEventDevice] FOREIGN KEY([Partition_Id])
REFERENCES [dbo].[Partitions] ([Id])
GO
ALTER TABLE [dbo].[EventDevices] CHECK CONSTRAINT [FK_PartitionEventDevice]
GO
ALTER TABLE [dbo].[GammaLightElement]  WITH CHECK ADD  CONSTRAINT [FK_GammaLightElement_Gamma] FOREIGN KEY([Gammas_Id])
REFERENCES [dbo].[Gammas] ([Id])
GO
ALTER TABLE [dbo].[GammaLightElement] CHECK CONSTRAINT [FK_GammaLightElement_Gamma]
GO
ALTER TABLE [dbo].[GammaLightElement]  WITH CHECK ADD  CONSTRAINT [FK_GammaLightElement_LightElement] FOREIGN KEY([LightElements_Id])
REFERENCES [dbo].[LightElements] ([Id])
GO
ALTER TABLE [dbo].[GammaLightElement] CHECK CONSTRAINT [FK_GammaLightElement_LightElement]
GO
ALTER TABLE [dbo].[LE_Types]  WITH CHECK ADD  CONSTRAINT [FK_ControlSpaceLE_Type] FOREIGN KEY([ControlSpace_Id])
REFERENCES [dbo].[ControlSpaces] ([Id])
GO
ALTER TABLE [dbo].[LE_Types] CHECK CONSTRAINT [FK_ControlSpaceLE_Type]
GO
ALTER TABLE [dbo].[LightElementLightZone]  WITH CHECK ADD  CONSTRAINT [FK_LightElementLightZone_LightElement] FOREIGN KEY([LightElements_Id])
REFERENCES [dbo].[LightElements] ([Id])
GO
ALTER TABLE [dbo].[LightElementLightZone] CHECK CONSTRAINT [FK_LightElementLightZone_LightElement]
GO
ALTER TABLE [dbo].[LightElementLightZone]  WITH CHECK ADD  CONSTRAINT [FK_LightElementLightZone_LightZone] FOREIGN KEY([LightZones_Id])
REFERENCES [dbo].[LightZones] ([Id])
GO
ALTER TABLE [dbo].[LightElementLightZone] CHECK CONSTRAINT [FK_LightElementLightZone_LightZone]
GO
ALTER TABLE [dbo].[LightElements]  WITH CHECK ADD  CONSTRAINT [FK_ControlSpaceLightElement] FOREIGN KEY([ControlSpace_Id])
REFERENCES [dbo].[ControlSpaces] ([Id])
GO
ALTER TABLE [dbo].[LightElements] CHECK CONSTRAINT [FK_ControlSpaceLightElement]
GO
ALTER TABLE [dbo].[LightElements]  WITH CHECK ADD  CONSTRAINT [FK_LightChannelLightElement] FOREIGN KEY([ControlChannel_Id])
REFERENCES [dbo].[ControlChannels] ([Id])
GO
ALTER TABLE [dbo].[LightElements] CHECK CONSTRAINT [FK_LightChannelLightElement]
GO
ALTER TABLE [dbo].[LightElements]  WITH CHECK ADD  CONSTRAINT [FK_PartitionLightElement] FOREIGN KEY([Partition_Id])
REFERENCES [dbo].[Partitions] ([Id])
GO
ALTER TABLE [dbo].[LightElements] CHECK CONSTRAINT [FK_PartitionLightElement]
GO
ALTER TABLE [dbo].[LightElements_LightStrip]  WITH CHECK ADD  CONSTRAINT [FK_LightStrip_inherits_LightElement] FOREIGN KEY([Id])
REFERENCES [dbo].[LightElements] ([Id])
ON DELETE CASCADE
GO
ALTER TABLE [dbo].[LightElements_LightStrip] CHECK CONSTRAINT [FK_LightStrip_inherits_LightElement]
GO
ALTER TABLE [dbo].[LightZoneEffect]  WITH CHECK ADD  CONSTRAINT [FK_LightZoneEffect_Effect] FOREIGN KEY([Effects_Id])
REFERENCES [dbo].[Effects] ([Id])
GO
ALTER TABLE [dbo].[LightZoneEffect] CHECK CONSTRAINT [FK_LightZoneEffect_Effect]
GO
ALTER TABLE [dbo].[LightZoneEffect]  WITH CHECK ADD  CONSTRAINT [FK_LightZoneEffect_LightZone] FOREIGN KEY([LightZones_Id])
REFERENCES [dbo].[LightZones] ([Id])
GO
ALTER TABLE [dbo].[LightZoneEffect] CHECK CONSTRAINT [FK_LightZoneEffect_LightZone]
GO
ALTER TABLE [dbo].[LightZones]  WITH CHECK ADD  CONSTRAINT [FK_PartitionLightZone] FOREIGN KEY([Partition_Id])
REFERENCES [dbo].[Partitions] ([Id])
GO
ALTER TABLE [dbo].[LightZones] CHECK CONSTRAINT [FK_PartitionLightZone]
GO
ALTER TABLE [dbo].[SceneLightZone]  WITH CHECK ADD  CONSTRAINT [FK_SceneLightZone_LightZone] FOREIGN KEY([LightZones_Id])
REFERENCES [dbo].[LightZones] ([Id])
GO
ALTER TABLE [dbo].[SceneLightZone] CHECK CONSTRAINT [FK_SceneLightZone_LightZone]
GO
ALTER TABLE [dbo].[SceneLightZone]  WITH CHECK ADD  CONSTRAINT [FK_SceneLightZone_Scene] FOREIGN KEY([Scenes_Id])
REFERENCES [dbo].[Scenes] ([Id])
GO
ALTER TABLE [dbo].[SceneLightZone] CHECK CONSTRAINT [FK_SceneLightZone_Scene]
GO
ALTER TABLE [dbo].[Scenes]  WITH CHECK ADD  CONSTRAINT [FK_PartitionScene] FOREIGN KEY([Partition_Id])
REFERENCES [dbo].[Partitions] ([Id])
GO
ALTER TABLE [dbo].[Scenes] CHECK CONSTRAINT [FK_PartitionScene]
GO
