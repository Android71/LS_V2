
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 06/21/2016 19:20:01
-- Generated from EDMX file: C:\Users\Андрей\Source\Repos\LS_V2\EFData\LSModel.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [LightSystemV1];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[FK_GammaLightElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LightElements] DROP CONSTRAINT [FK_GammaLightElement];
GO
IF OBJECT_ID(N'[dbo].[FK_SceneLightZone_Scene]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SceneLightZone] DROP CONSTRAINT [FK_SceneLightZone_Scene];
GO
IF OBJECT_ID(N'[dbo].[FK_SceneLightZone_LightZone]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[SceneLightZone] DROP CONSTRAINT [FK_SceneLightZone_LightZone];
GO
IF OBJECT_ID(N'[dbo].[FK_PartitionScene]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Scenes] DROP CONSTRAINT [FK_PartitionScene];
GO
IF OBJECT_ID(N'[dbo].[FK_LightChannelLightElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LightElements] DROP CONSTRAINT [FK_LightChannelLightElement];
GO
IF OBJECT_ID(N'[dbo].[FK_EventChannelLightZone_EventChannel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventChannelLightZone] DROP CONSTRAINT [FK_EventChannelLightZone_EventChannel];
GO
IF OBJECT_ID(N'[dbo].[FK_EventChannelLightZone_LightZone]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventChannelLightZone] DROP CONSTRAINT [FK_EventChannelLightZone_LightZone];
GO
IF OBJECT_ID(N'[dbo].[FK_PartitionLightZone]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LightZones] DROP CONSTRAINT [FK_PartitionLightZone];
GO
IF OBJECT_ID(N'[dbo].[FK_PartitionLightElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LightElements] DROP CONSTRAINT [FK_PartitionLightElement];
GO
IF OBJECT_ID(N'[dbo].[FK_ControlSpaceLightElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LightElements] DROP CONSTRAINT [FK_ControlSpaceLightElement];
GO
IF OBJECT_ID(N'[dbo].[FK_LightZoneEffect]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Effects] DROP CONSTRAINT [FK_LightZoneEffect];
GO
IF OBJECT_ID(N'[dbo].[FK_ControlSpaceLightZone]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LightZones] DROP CONSTRAINT [FK_ControlSpaceLightZone];
GO
IF OBJECT_ID(N'[dbo].[FK_SceneEffect]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[Effects] DROP CONSTRAINT [FK_SceneEffect];
GO
IF OBJECT_ID(N'[dbo].[FK_EffectPartTypeEffectPart]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EffectParts] DROP CONSTRAINT [FK_EffectPartTypeEffectPart];
GO
IF OBJECT_ID(N'[dbo].[FK_EffectEffectPart]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EffectParts] DROP CONSTRAINT [FK_EffectEffectPart];
GO
IF OBJECT_ID(N'[dbo].[FK_EffectPartEffectPart]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EffectParts] DROP CONSTRAINT [FK_EffectPartEffectPart];
GO
IF OBJECT_ID(N'[dbo].[FK_LightZoneLE_Proxy]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LE_Proxies] DROP CONSTRAINT [FK_LightZoneLE_Proxy];
GO
IF OBJECT_ID(N'[dbo].[FK_LightElementLE_Proxy]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LE_Proxies] DROP CONSTRAINT [FK_LightElementLE_Proxy];
GO
IF OBJECT_ID(N'[dbo].[FK_CustomGammaLightElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LightElements] DROP CONSTRAINT [FK_CustomGammaLightElement];
GO
IF OBJECT_ID(N'[dbo].[FK_ControlSpaceControlChannel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ControlChannels] DROP CONSTRAINT [FK_ControlSpaceControlChannel];
GO
IF OBJECT_ID(N'[dbo].[FK_ControlSpaceEventChannel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventChannels] DROP CONSTRAINT [FK_ControlSpaceEventChannel];
GO
IF OBJECT_ID(N'[dbo].[FK_ControlSpaceLightPointType]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LE_Types] DROP CONSTRAINT [FK_ControlSpaceLightPointType];
GO
IF OBJECT_ID(N'[dbo].[FK_ControlSpaceControlDevice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ControlDevices] DROP CONSTRAINT [FK_ControlSpaceControlDevice];
GO
IF OBJECT_ID(N'[dbo].[FK_ControlDeviceControlChannel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ControlChannels] DROP CONSTRAINT [FK_ControlDeviceControlChannel];
GO
IF OBJECT_ID(N'[dbo].[FK_ControlSpaceEventDevice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventDevices] DROP CONSTRAINT [FK_ControlSpaceEventDevice];
GO
IF OBJECT_ID(N'[dbo].[FK_EventDeviceEventChannel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventChannels] DROP CONSTRAINT [FK_EventDeviceEventChannel];
GO
IF OBJECT_ID(N'[dbo].[FK_CSEnvItemEnvironmentItem]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CSEnvItems] DROP CONSTRAINT [FK_CSEnvItemEnvironmentItem];
GO
IF OBJECT_ID(N'[dbo].[FK_CSEnvItemControlSpace]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[CSEnvItems] DROP CONSTRAINT [FK_CSEnvItemControlSpace];
GO
IF OBJECT_ID(N'[dbo].[FK_PartitionEventDevice]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[EventDevices] DROP CONSTRAINT [FK_PartitionEventDevice];
GO
IF OBJECT_ID(N'[dbo].[FK_ArtNetControlChannel_inherits_ControlChannel]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[ControlChannels_ArtNetControlChannel] DROP CONSTRAINT [FK_ArtNetControlChannel_inherits_ControlChannel];
GO
IF OBJECT_ID(N'[dbo].[FK_LightStrip_inherits_LightElement]', 'F') IS NOT NULL
    ALTER TABLE [dbo].[LightElements_LightStrip] DROP CONSTRAINT [FK_LightStrip_inherits_LightElement];
GO

-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[ControlSpaces]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ControlSpaces];
GO
IF OBJECT_ID(N'[dbo].[ControlChannels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ControlChannels];
GO
IF OBJECT_ID(N'[dbo].[EventChannels]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventChannels];
GO
IF OBJECT_ID(N'[dbo].[LightElements]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LightElements];
GO
IF OBJECT_ID(N'[dbo].[LightZones]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LightZones];
GO
IF OBJECT_ID(N'[dbo].[Gammas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Gammas];
GO
IF OBJECT_ID(N'[dbo].[Partitions]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Partitions];
GO
IF OBJECT_ID(N'[dbo].[BaseEffects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[BaseEffects];
GO
IF OBJECT_ID(N'[dbo].[Patterns]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Patterns];
GO
IF OBJECT_ID(N'[dbo].[Scenes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Scenes];
GO
IF OBJECT_ID(N'[dbo].[Effects]', 'U') IS NOT NULL
    DROP TABLE [dbo].[Effects];
GO
IF OBJECT_ID(N'[dbo].[EnvironmentItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EnvironmentItems];
GO
IF OBJECT_ID(N'[dbo].[LE_Types]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LE_Types];
GO
IF OBJECT_ID(N'[dbo].[EffectParts]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EffectParts];
GO
IF OBJECT_ID(N'[dbo].[EffectPartTypes]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EffectPartTypes];
GO
IF OBJECT_ID(N'[dbo].[LE_Proxies]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LE_Proxies];
GO
IF OBJECT_ID(N'[dbo].[CustomGammas]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CustomGammas];
GO
IF OBJECT_ID(N'[dbo].[ControlDevices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ControlDevices];
GO
IF OBJECT_ID(N'[dbo].[EventDevices]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventDevices];
GO
IF OBJECT_ID(N'[dbo].[CSEnvItems]', 'U') IS NOT NULL
    DROP TABLE [dbo].[CSEnvItems];
GO
IF OBJECT_ID(N'[dbo].[ControlChannels_ArtNetControlChannel]', 'U') IS NOT NULL
    DROP TABLE [dbo].[ControlChannels_ArtNetControlChannel];
GO
IF OBJECT_ID(N'[dbo].[LightElements_LightStrip]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LightElements_LightStrip];
GO
IF OBJECT_ID(N'[dbo].[SceneLightZone]', 'U') IS NOT NULL
    DROP TABLE [dbo].[SceneLightZone];
GO
IF OBJECT_ID(N'[dbo].[EventChannelLightZone]', 'U') IS NOT NULL
    DROP TABLE [dbo].[EventChannelLightZone];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'ControlSpaces'
CREATE TABLE [dbo].[ControlSpaces] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [IsActive] bit  NOT NULL
);
GO

-- Creating table 'ControlChannels'
CREATE TABLE [dbo].[ControlChannels] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ChannelNo] int  NOT NULL,
    [PointType] int  NULL,
    [HaveDimmer] bit  NULL,
    [DotNetType] nvarchar(max)  NULL,
    [Profile] nvarchar(max)  NULL,
    [ControlSpace_Id] int  NOT NULL,
    [ControlDevice_Id] int  NOT NULL,
    [Partition_Id] int  NOT NULL
);
GO

-- Creating table 'EventChannels'
CREATE TABLE [dbo].[EventChannels] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [ChannelNo] int  NOT NULL,
    [EventName] nvarchar(max)  NOT NULL,
    [DotNetType] nvarchar(max)  NOT NULL,
    [Profile] nvarchar(max)  NOT NULL,
    [ControlSpace_Id] int  NOT NULL,
    [EventDevice_Id] int  NOT NULL,
    [Partition_Id] int  NOT NULL
);
GO

-- Creating table 'LightElements'
CREATE TABLE [dbo].[LightElements] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [CanDimming] bit  NOT NULL,
    [PointType] int  NULL,
    [Remark] nvarchar(max)  NULL,
    [Gamma_Id] int  NULL,
    [ControlChannel_Id] int  NULL,
    [Partition_Id] int  NOT NULL,
    [ControlSpace_Id] int  NOT NULL,
    [CustomGamma_Id] int  NULL
);
GO

-- Creating table 'LightZones'
CREATE TABLE [dbo].[LightZones] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [IsNode] bit  NOT NULL,
    [PointType] int  NULL,
    [Remark] nvarchar(max)  NULL,
    [Partition_Id] int  NOT NULL,
    [ControlSpace_Id] int  NOT NULL
);
GO

-- Creating table 'Gammas'
CREATE TABLE [dbo].[Gammas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Value] varbinary(max)  NULL
);
GO

-- Creating table 'Partitions'
CREATE TABLE [dbo].[Partitions] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'BaseEffects'
CREATE TABLE [dbo].[BaseEffects] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Category] nvarchar(max)  NOT NULL,
    [Params] nvarchar(max)  NOT NULL,
    [PointType] int  NOT NULL,
    [Remark] nvarchar(max)  NULL
);
GO

-- Creating table 'Patterns'
CREATE TABLE [dbo].[Patterns] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Value] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'Scenes'
CREATE TABLE [dbo].[Scenes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Remark] nvarchar(max)  NULL,
    [Partition_Id] int  NOT NULL
);
GO

-- Creating table 'Effects'
CREATE TABLE [dbo].[Effects] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Category] nvarchar(max)  NOT NULL,
    [Params] nvarchar(max)  NOT NULL,
    [PointType] int  NOT NULL,
    [Type] nvarchar(max)  NULL,
    [Remark] nvarchar(max)  NULL,
    [LightZone_Id] int  NULL,
    [Scene_Id] int  NOT NULL
);
GO

-- Creating table 'EnvironmentItems'
CREATE TABLE [dbo].[EnvironmentItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Model] nvarchar(max)  NOT NULL,
    [DeviceType] int  NOT NULL,
    [Profile] nvarchar(max)  NOT NULL,
    [DotNetType] nvarchar(max)  NULL
);
GO

-- Creating table 'LE_Types'
CREATE TABLE [dbo].[LE_Types] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [PointType] int  NOT NULL,
    [IsActive] bit  NOT NULL,
    [Remark] nvarchar(max)  NULL,
    [ControlSpace_Id] int  NOT NULL
);
GO

-- Creating table 'EffectParts'
CREATE TABLE [dbo].[EffectParts] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Params] nvarchar(max)  NULL,
    [EffectPartType_Id] int  NOT NULL,
    [Effect_Id] int  NOT NULL,
    [EffectPartEffectPart_EffectPart1_Id] int  NOT NULL
);
GO

-- Creating table 'EffectPartTypes'
CREATE TABLE [dbo].[EffectPartTypes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Category] nvarchar(max)  NULL,
    [Params] nvarchar(max)  NULL,
    [PointType] int  NOT NULL
);
GO

-- Creating table 'LE_Proxies'
CREATE TABLE [dbo].[LE_Proxies] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Ix] int  NOT NULL,
    [LightZone_Id] int  NULL,
    [LightElement_Id] int  NOT NULL
);
GO

-- Creating table 'CustomGammas'
CREATE TABLE [dbo].[CustomGammas] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NULL,
    [Value] varbinary(max)  NULL,
    [ValueR] varbinary(max)  NULL,
    [ValueG] varbinary(max)  NULL,
    [ValueB] varbinary(max)  NULL
);
GO

-- Creating table 'ControlDevices'
CREATE TABLE [dbo].[ControlDevices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Model] nvarchar(max)  NOT NULL,
    [HaveDimmer] bit  NOT NULL,
    [Profile] nvarchar(max)  NULL,
    [Remark] nvarchar(max)  NULL,
    [MultiChannel] bit  NOT NULL,
    [CanAddChannel] bit  NOT NULL,
    [DotNetType] nvarchar(max)  NULL,
    [ControlSpace_Id] int  NOT NULL,
    [Partition_Id] int  NOT NULL
);
GO

-- Creating table 'EventDevices'
CREATE TABLE [dbo].[EventDevices] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Name] nvarchar(max)  NOT NULL,
    [Model] nvarchar(max)  NOT NULL,
    [Mode] int  NOT NULL,
    [Profile] nvarchar(max)  NOT NULL,
    [Remark] nvarchar(max)  NULL,
    [MultiChannel] bit  NOT NULL,
    [CanAddChannel] bit  NOT NULL,
    [DotNetType] nvarchar(max)  NOT NULL,
    [ControlSpace_Id] int  NOT NULL,
    [Partition_Id] int  NOT NULL
);
GO

-- Creating table 'CSEnvItems'
CREATE TABLE [dbo].[CSEnvItems] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [EnvironmentItem_Id] int  NOT NULL,
    [ControlSpace_Id] int  NOT NULL
);
GO

-- Creating table 'ControlChannels_ArtNetControlChannel'
CREATE TABLE [dbo].[ControlChannels_ArtNetControlChannel] (
    [IPAddress] nvarchar(max)  NOT NULL,
    [PortNo] int  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'LightElements_LightStrip'
CREATE TABLE [dbo].[LightElements_LightStrip] (
    [StartPoint] int  NOT NULL,
    [PointCount] int  NOT NULL,
    [Direction] int  NOT NULL,
    [ColorSequence] nvarchar(max)  NOT NULL,
    [Id] int  NOT NULL
);
GO

-- Creating table 'SceneLightZone'
CREATE TABLE [dbo].[SceneLightZone] (
    [Scenes_Id] int  NOT NULL,
    [LightZones_Id] int  NOT NULL
);
GO

-- Creating table 'EventChannelLightZone'
CREATE TABLE [dbo].[EventChannelLightZone] (
    [EventChannels_Id] int  NOT NULL,
    [LightZones_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'ControlSpaces'
ALTER TABLE [dbo].[ControlSpaces]
ADD CONSTRAINT [PK_ControlSpaces]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ControlChannels'
ALTER TABLE [dbo].[ControlChannels]
ADD CONSTRAINT [PK_ControlChannels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventChannels'
ALTER TABLE [dbo].[EventChannels]
ADD CONSTRAINT [PK_EventChannels]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LightElements'
ALTER TABLE [dbo].[LightElements]
ADD CONSTRAINT [PK_LightElements]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LightZones'
ALTER TABLE [dbo].[LightZones]
ADD CONSTRAINT [PK_LightZones]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Gammas'
ALTER TABLE [dbo].[Gammas]
ADD CONSTRAINT [PK_Gammas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Partitions'
ALTER TABLE [dbo].[Partitions]
ADD CONSTRAINT [PK_Partitions]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'BaseEffects'
ALTER TABLE [dbo].[BaseEffects]
ADD CONSTRAINT [PK_BaseEffects]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Patterns'
ALTER TABLE [dbo].[Patterns]
ADD CONSTRAINT [PK_Patterns]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Scenes'
ALTER TABLE [dbo].[Scenes]
ADD CONSTRAINT [PK_Scenes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Effects'
ALTER TABLE [dbo].[Effects]
ADD CONSTRAINT [PK_Effects]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EnvironmentItems'
ALTER TABLE [dbo].[EnvironmentItems]
ADD CONSTRAINT [PK_EnvironmentItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LE_Types'
ALTER TABLE [dbo].[LE_Types]
ADD CONSTRAINT [PK_LE_Types]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EffectParts'
ALTER TABLE [dbo].[EffectParts]
ADD CONSTRAINT [PK_EffectParts]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EffectPartTypes'
ALTER TABLE [dbo].[EffectPartTypes]
ADD CONSTRAINT [PK_EffectPartTypes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LE_Proxies'
ALTER TABLE [dbo].[LE_Proxies]
ADD CONSTRAINT [PK_LE_Proxies]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CustomGammas'
ALTER TABLE [dbo].[CustomGammas]
ADD CONSTRAINT [PK_CustomGammas]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ControlDevices'
ALTER TABLE [dbo].[ControlDevices]
ADD CONSTRAINT [PK_ControlDevices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'EventDevices'
ALTER TABLE [dbo].[EventDevices]
ADD CONSTRAINT [PK_EventDevices]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'CSEnvItems'
ALTER TABLE [dbo].[CSEnvItems]
ADD CONSTRAINT [PK_CSEnvItems]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'ControlChannels_ArtNetControlChannel'
ALTER TABLE [dbo].[ControlChannels_ArtNetControlChannel]
ADD CONSTRAINT [PK_ControlChannels_ArtNetControlChannel]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'LightElements_LightStrip'
ALTER TABLE [dbo].[LightElements_LightStrip]
ADD CONSTRAINT [PK_LightElements_LightStrip]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Scenes_Id], [LightZones_Id] in table 'SceneLightZone'
ALTER TABLE [dbo].[SceneLightZone]
ADD CONSTRAINT [PK_SceneLightZone]
    PRIMARY KEY CLUSTERED ([Scenes_Id], [LightZones_Id] ASC);
GO

-- Creating primary key on [EventChannels_Id], [LightZones_Id] in table 'EventChannelLightZone'
ALTER TABLE [dbo].[EventChannelLightZone]
ADD CONSTRAINT [PK_EventChannelLightZone]
    PRIMARY KEY CLUSTERED ([EventChannels_Id], [LightZones_Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [Gamma_Id] in table 'LightElements'
ALTER TABLE [dbo].[LightElements]
ADD CONSTRAINT [FK_GammaLightElement]
    FOREIGN KEY ([Gamma_Id])
    REFERENCES [dbo].[Gammas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_GammaLightElement'
CREATE INDEX [IX_FK_GammaLightElement]
ON [dbo].[LightElements]
    ([Gamma_Id]);
GO

-- Creating foreign key on [Scenes_Id] in table 'SceneLightZone'
ALTER TABLE [dbo].[SceneLightZone]
ADD CONSTRAINT [FK_SceneLightZone_Scene]
    FOREIGN KEY ([Scenes_Id])
    REFERENCES [dbo].[Scenes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [LightZones_Id] in table 'SceneLightZone'
ALTER TABLE [dbo].[SceneLightZone]
ADD CONSTRAINT [FK_SceneLightZone_LightZone]
    FOREIGN KEY ([LightZones_Id])
    REFERENCES [dbo].[LightZones]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SceneLightZone_LightZone'
CREATE INDEX [IX_FK_SceneLightZone_LightZone]
ON [dbo].[SceneLightZone]
    ([LightZones_Id]);
GO

-- Creating foreign key on [Partition_Id] in table 'Scenes'
ALTER TABLE [dbo].[Scenes]
ADD CONSTRAINT [FK_PartitionScene]
    FOREIGN KEY ([Partition_Id])
    REFERENCES [dbo].[Partitions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartitionScene'
CREATE INDEX [IX_FK_PartitionScene]
ON [dbo].[Scenes]
    ([Partition_Id]);
GO

-- Creating foreign key on [ControlChannel_Id] in table 'LightElements'
ALTER TABLE [dbo].[LightElements]
ADD CONSTRAINT [FK_LightChannelLightElement]
    FOREIGN KEY ([ControlChannel_Id])
    REFERENCES [dbo].[ControlChannels]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LightChannelLightElement'
CREATE INDEX [IX_FK_LightChannelLightElement]
ON [dbo].[LightElements]
    ([ControlChannel_Id]);
GO

-- Creating foreign key on [EventChannels_Id] in table 'EventChannelLightZone'
ALTER TABLE [dbo].[EventChannelLightZone]
ADD CONSTRAINT [FK_EventChannelLightZone_EventChannel]
    FOREIGN KEY ([EventChannels_Id])
    REFERENCES [dbo].[EventChannels]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating foreign key on [LightZones_Id] in table 'EventChannelLightZone'
ALTER TABLE [dbo].[EventChannelLightZone]
ADD CONSTRAINT [FK_EventChannelLightZone_LightZone]
    FOREIGN KEY ([LightZones_Id])
    REFERENCES [dbo].[LightZones]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EventChannelLightZone_LightZone'
CREATE INDEX [IX_FK_EventChannelLightZone_LightZone]
ON [dbo].[EventChannelLightZone]
    ([LightZones_Id]);
GO

-- Creating foreign key on [Partition_Id] in table 'LightZones'
ALTER TABLE [dbo].[LightZones]
ADD CONSTRAINT [FK_PartitionLightZone]
    FOREIGN KEY ([Partition_Id])
    REFERENCES [dbo].[Partitions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartitionLightZone'
CREATE INDEX [IX_FK_PartitionLightZone]
ON [dbo].[LightZones]
    ([Partition_Id]);
GO

-- Creating foreign key on [Partition_Id] in table 'LightElements'
ALTER TABLE [dbo].[LightElements]
ADD CONSTRAINT [FK_PartitionLightElement]
    FOREIGN KEY ([Partition_Id])
    REFERENCES [dbo].[Partitions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartitionLightElement'
CREATE INDEX [IX_FK_PartitionLightElement]
ON [dbo].[LightElements]
    ([Partition_Id]);
GO

-- Creating foreign key on [ControlSpace_Id] in table 'LightElements'
ALTER TABLE [dbo].[LightElements]
ADD CONSTRAINT [FK_ControlSpaceLightElement]
    FOREIGN KEY ([ControlSpace_Id])
    REFERENCES [dbo].[ControlSpaces]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ControlSpaceLightElement'
CREATE INDEX [IX_FK_ControlSpaceLightElement]
ON [dbo].[LightElements]
    ([ControlSpace_Id]);
GO

-- Creating foreign key on [LightZone_Id] in table 'Effects'
ALTER TABLE [dbo].[Effects]
ADD CONSTRAINT [FK_LightZoneEffect]
    FOREIGN KEY ([LightZone_Id])
    REFERENCES [dbo].[LightZones]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LightZoneEffect'
CREATE INDEX [IX_FK_LightZoneEffect]
ON [dbo].[Effects]
    ([LightZone_Id]);
GO

-- Creating foreign key on [ControlSpace_Id] in table 'LightZones'
ALTER TABLE [dbo].[LightZones]
ADD CONSTRAINT [FK_ControlSpaceLightZone]
    FOREIGN KEY ([ControlSpace_Id])
    REFERENCES [dbo].[ControlSpaces]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ControlSpaceLightZone'
CREATE INDEX [IX_FK_ControlSpaceLightZone]
ON [dbo].[LightZones]
    ([ControlSpace_Id]);
GO

-- Creating foreign key on [Scene_Id] in table 'Effects'
ALTER TABLE [dbo].[Effects]
ADD CONSTRAINT [FK_SceneEffect]
    FOREIGN KEY ([Scene_Id])
    REFERENCES [dbo].[Scenes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_SceneEffect'
CREATE INDEX [IX_FK_SceneEffect]
ON [dbo].[Effects]
    ([Scene_Id]);
GO

-- Creating foreign key on [EffectPartType_Id] in table 'EffectParts'
ALTER TABLE [dbo].[EffectParts]
ADD CONSTRAINT [FK_EffectPartTypeEffectPart]
    FOREIGN KEY ([EffectPartType_Id])
    REFERENCES [dbo].[EffectPartTypes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EffectPartTypeEffectPart'
CREATE INDEX [IX_FK_EffectPartTypeEffectPart]
ON [dbo].[EffectParts]
    ([EffectPartType_Id]);
GO

-- Creating foreign key on [Effect_Id] in table 'EffectParts'
ALTER TABLE [dbo].[EffectParts]
ADD CONSTRAINT [FK_EffectEffectPart]
    FOREIGN KEY ([Effect_Id])
    REFERENCES [dbo].[Effects]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EffectEffectPart'
CREATE INDEX [IX_FK_EffectEffectPart]
ON [dbo].[EffectParts]
    ([Effect_Id]);
GO

-- Creating foreign key on [EffectPartEffectPart_EffectPart1_Id] in table 'EffectParts'
ALTER TABLE [dbo].[EffectParts]
ADD CONSTRAINT [FK_EffectPartEffectPart]
    FOREIGN KEY ([EffectPartEffectPart_EffectPart1_Id])
    REFERENCES [dbo].[EffectParts]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EffectPartEffectPart'
CREATE INDEX [IX_FK_EffectPartEffectPart]
ON [dbo].[EffectParts]
    ([EffectPartEffectPart_EffectPart1_Id]);
GO

-- Creating foreign key on [LightZone_Id] in table 'LE_Proxies'
ALTER TABLE [dbo].[LE_Proxies]
ADD CONSTRAINT [FK_LightZoneLE_Proxy]
    FOREIGN KEY ([LightZone_Id])
    REFERENCES [dbo].[LightZones]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LightZoneLE_Proxy'
CREATE INDEX [IX_FK_LightZoneLE_Proxy]
ON [dbo].[LE_Proxies]
    ([LightZone_Id]);
GO

-- Creating foreign key on [LightElement_Id] in table 'LE_Proxies'
ALTER TABLE [dbo].[LE_Proxies]
ADD CONSTRAINT [FK_LightElementLE_Proxy]
    FOREIGN KEY ([LightElement_Id])
    REFERENCES [dbo].[LightElements]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_LightElementLE_Proxy'
CREATE INDEX [IX_FK_LightElementLE_Proxy]
ON [dbo].[LE_Proxies]
    ([LightElement_Id]);
GO

-- Creating foreign key on [CustomGamma_Id] in table 'LightElements'
ALTER TABLE [dbo].[LightElements]
ADD CONSTRAINT [FK_CustomGammaLightElement]
    FOREIGN KEY ([CustomGamma_Id])
    REFERENCES [dbo].[CustomGammas]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CustomGammaLightElement'
CREATE INDEX [IX_FK_CustomGammaLightElement]
ON [dbo].[LightElements]
    ([CustomGamma_Id]);
GO

-- Creating foreign key on [ControlSpace_Id] in table 'ControlChannels'
ALTER TABLE [dbo].[ControlChannels]
ADD CONSTRAINT [FK_ControlSpaceControlChannel]
    FOREIGN KEY ([ControlSpace_Id])
    REFERENCES [dbo].[ControlSpaces]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ControlSpaceControlChannel'
CREATE INDEX [IX_FK_ControlSpaceControlChannel]
ON [dbo].[ControlChannels]
    ([ControlSpace_Id]);
GO

-- Creating foreign key on [ControlSpace_Id] in table 'EventChannels'
ALTER TABLE [dbo].[EventChannels]
ADD CONSTRAINT [FK_ControlSpaceEventChannel]
    FOREIGN KEY ([ControlSpace_Id])
    REFERENCES [dbo].[ControlSpaces]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ControlSpaceEventChannel'
CREATE INDEX [IX_FK_ControlSpaceEventChannel]
ON [dbo].[EventChannels]
    ([ControlSpace_Id]);
GO

-- Creating foreign key on [ControlSpace_Id] in table 'LE_Types'
ALTER TABLE [dbo].[LE_Types]
ADD CONSTRAINT [FK_ControlSpaceLightPointType]
    FOREIGN KEY ([ControlSpace_Id])
    REFERENCES [dbo].[ControlSpaces]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ControlSpaceLightPointType'
CREATE INDEX [IX_FK_ControlSpaceLightPointType]
ON [dbo].[LE_Types]
    ([ControlSpace_Id]);
GO

-- Creating foreign key on [ControlSpace_Id] in table 'ControlDevices'
ALTER TABLE [dbo].[ControlDevices]
ADD CONSTRAINT [FK_ControlSpaceControlDevice]
    FOREIGN KEY ([ControlSpace_Id])
    REFERENCES [dbo].[ControlSpaces]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ControlSpaceControlDevice'
CREATE INDEX [IX_FK_ControlSpaceControlDevice]
ON [dbo].[ControlDevices]
    ([ControlSpace_Id]);
GO

-- Creating foreign key on [ControlDevice_Id] in table 'ControlChannels'
ALTER TABLE [dbo].[ControlChannels]
ADD CONSTRAINT [FK_ControlDeviceControlChannel]
    FOREIGN KEY ([ControlDevice_Id])
    REFERENCES [dbo].[ControlDevices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ControlDeviceControlChannel'
CREATE INDEX [IX_FK_ControlDeviceControlChannel]
ON [dbo].[ControlChannels]
    ([ControlDevice_Id]);
GO

-- Creating foreign key on [ControlSpace_Id] in table 'EventDevices'
ALTER TABLE [dbo].[EventDevices]
ADD CONSTRAINT [FK_ControlSpaceEventDevice]
    FOREIGN KEY ([ControlSpace_Id])
    REFERENCES [dbo].[ControlSpaces]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_ControlSpaceEventDevice'
CREATE INDEX [IX_FK_ControlSpaceEventDevice]
ON [dbo].[EventDevices]
    ([ControlSpace_Id]);
GO

-- Creating foreign key on [EventDevice_Id] in table 'EventChannels'
ALTER TABLE [dbo].[EventChannels]
ADD CONSTRAINT [FK_EventDeviceEventChannel]
    FOREIGN KEY ([EventDevice_Id])
    REFERENCES [dbo].[EventDevices]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_EventDeviceEventChannel'
CREATE INDEX [IX_FK_EventDeviceEventChannel]
ON [dbo].[EventChannels]
    ([EventDevice_Id]);
GO

-- Creating foreign key on [EnvironmentItem_Id] in table 'CSEnvItems'
ALTER TABLE [dbo].[CSEnvItems]
ADD CONSTRAINT [FK_CSEnvItemEnvironmentItem]
    FOREIGN KEY ([EnvironmentItem_Id])
    REFERENCES [dbo].[EnvironmentItems]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CSEnvItemEnvironmentItem'
CREATE INDEX [IX_FK_CSEnvItemEnvironmentItem]
ON [dbo].[CSEnvItems]
    ([EnvironmentItem_Id]);
GO

-- Creating foreign key on [ControlSpace_Id] in table 'CSEnvItems'
ALTER TABLE [dbo].[CSEnvItems]
ADD CONSTRAINT [FK_CSEnvItemControlSpace]
    FOREIGN KEY ([ControlSpace_Id])
    REFERENCES [dbo].[ControlSpaces]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_CSEnvItemControlSpace'
CREATE INDEX [IX_FK_CSEnvItemControlSpace]
ON [dbo].[CSEnvItems]
    ([ControlSpace_Id]);
GO

-- Creating foreign key on [Partition_Id] in table 'EventDevices'
ALTER TABLE [dbo].[EventDevices]
ADD CONSTRAINT [FK_PartitionEventDevice]
    FOREIGN KEY ([Partition_Id])
    REFERENCES [dbo].[Partitions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartitionEventDevice'
CREATE INDEX [IX_FK_PartitionEventDevice]
ON [dbo].[EventDevices]
    ([Partition_Id]);
GO

-- Creating foreign key on [Partition_Id] in table 'EventChannels'
ALTER TABLE [dbo].[EventChannels]
ADD CONSTRAINT [FK_PartitionEventChannel]
    FOREIGN KEY ([Partition_Id])
    REFERENCES [dbo].[Partitions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartitionEventChannel'
CREATE INDEX [IX_FK_PartitionEventChannel]
ON [dbo].[EventChannels]
    ([Partition_Id]);
GO

-- Creating foreign key on [Partition_Id] in table 'ControlChannels'
ALTER TABLE [dbo].[ControlChannels]
ADD CONSTRAINT [FK_PartitionControlChannel]
    FOREIGN KEY ([Partition_Id])
    REFERENCES [dbo].[Partitions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartitionControlChannel'
CREATE INDEX [IX_FK_PartitionControlChannel]
ON [dbo].[ControlChannels]
    ([Partition_Id]);
GO

-- Creating foreign key on [Partition_Id] in table 'ControlDevices'
ALTER TABLE [dbo].[ControlDevices]
ADD CONSTRAINT [FK_PartitionControlDevice]
    FOREIGN KEY ([Partition_Id])
    REFERENCES [dbo].[Partitions]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_PartitionControlDevice'
CREATE INDEX [IX_FK_PartitionControlDevice]
ON [dbo].[ControlDevices]
    ([Partition_Id]);
GO

-- Creating foreign key on [Id] in table 'ControlChannels_ArtNetControlChannel'
ALTER TABLE [dbo].[ControlChannels_ArtNetControlChannel]
ADD CONSTRAINT [FK_ArtNetControlChannel_inherits_ControlChannel]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[ControlChannels]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- Creating foreign key on [Id] in table 'LightElements_LightStrip'
ALTER TABLE [dbo].[LightElements_LightStrip]
ADD CONSTRAINT [FK_LightStrip_inherits_LightElement]
    FOREIGN KEY ([Id])
    REFERENCES [dbo].[LightElements]
        ([Id])
    ON DELETE CASCADE ON UPDATE NO ACTION;
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------