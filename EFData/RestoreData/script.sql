USE [LightSystemV1]
GO
SET IDENTITY_INSERT [dbo].[ControlSpaces] ON 

INSERT [dbo].[ControlSpaces] ([Id], [Name], [IsActive], [Prefix]) VALUES (1, N'ArtNet', 1, N'AN')
INSERT [dbo].[ControlSpaces] ([Id], [Name], [IsActive], [Prefix]) VALUES (2, N'DMX', 0, N'DX')
INSERT [dbo].[ControlSpaces] ([Id], [Name], [IsActive], [Prefix]) VALUES (3, N'NooLite', 1, N'NL')
INSERT [dbo].[ControlSpaces] ([Id], [Name], [IsActive], [Prefix]) VALUES (4, N'MiLight', 0, N'ML')
SET IDENTITY_INSERT [dbo].[ControlSpaces] OFF
SET IDENTITY_INSERT [dbo].[Partitions] ON 

INSERT [dbo].[Partitions] ([Id], [Name]) VALUES (1, N'Кухня')
INSERT [dbo].[Partitions] ([Id], [Name]) VALUES (2, N'Гостиная')
INSERT [dbo].[Partitions] ([Id], [Name]) VALUES (3, N'Спальня')
SET IDENTITY_INSERT [dbo].[Partitions] OFF
SET IDENTITY_INSERT [dbo].[ControlDevices] ON 

INSERT [dbo].[ControlDevices] ([Id], [Name], [Model], [HaveDimmer], [Profile], [Remark], [MultiChannel], [CanAddChannel], [DotNetType], [ControlSpace_Id], [Partition_Id]) VALUES (1, N'AN_Dev1', N'AN6USPI', 1, N'<Params>
  <IPAddress Value="2.0.0.2" />
  <VirtualIP Value="2.0.0.3" />
</Params>', NULL, 1, 0, N'LS_Designer_WPF.Model.AN6USPI, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', 1, 1)
INSERT [dbo].[ControlDevices] ([Id], [Name], [Model], [HaveDimmer], [Profile], [Remark], [MultiChannel], [CanAddChannel], [DotNetType], [ControlSpace_Id], [Partition_Id]) VALUES (2, N'NL_PB1', N'SU111-500', 1, N'<Params Model="SU111-500" HaveDimmer="True" PointType="W" />', N'Потолок1', 0, 0, N'LS_Designer_WPF.Model.NLPowerBlock, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', 3, 1)
INSERT [dbo].[ControlDevices] ([Id], [Name], [Model], [HaveDimmer], [Profile], [Remark], [MultiChannel], [CanAddChannel], [DotNetType], [ControlSpace_Id], [Partition_Id]) VALUES (3, N'NL_PB2', N'SU111-500', 1, N'<Params Model="SU111-500" HaveDimmer="True" PointType="W" />', N'Потолок2', 0, 0, N'LS_Designer_WPF.Model.NLPowerBlock, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', 3, 1)
INSERT [dbo].[ControlDevices] ([Id], [Name], [Model], [HaveDimmer], [Profile], [Remark], [MultiChannel], [CanAddChannel], [DotNetType], [ControlSpace_Id], [Partition_Id]) VALUES (4, N'NL_PB3', N'SU111-500', 1, N'<Params Model="SU111-500" HaveDimmer="True" PointType="W" />', N'Потолок3', 0, 0, N'LS_Designer_WPF.Model.NLPowerBlock, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', 3, 1)
INSERT [dbo].[ControlDevices] ([Id], [Name], [Model], [HaveDimmer], [Profile], [Remark], [MultiChannel], [CanAddChannel], [DotNetType], [ControlSpace_Id], [Partition_Id]) VALUES (5, N'NL_PB4', N'SU111-500', 1, N'<Params Model="SU111-500" HaveDimmer="True" PointType="W" />', N'Столо', 0, 0, N'LS_Designer_WPF.Model.NLPowerBlock, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', 3, 1)
INSERT [dbo].[ControlDevices] ([Id], [Name], [Model], [HaveDimmer], [Profile], [Remark], [MultiChannel], [CanAddChannel], [DotNetType], [ControlSpace_Id], [Partition_Id]) VALUES (6, N'NL_PBrgb1', N'SD111-180', 1, N'<Params Model="SD111-180" HaveDimmer="True" PointType="RGB" />', N'Мойка', 0, 0, N'LS_Designer_WPF.Model.NLPowerBlock, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', 3, 1)
SET IDENTITY_INSERT [dbo].[ControlDevices] OFF
SET IDENTITY_INSERT [dbo].[ControlChannels] ON 

INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (1, N'Universe_0', 0, 3, 1, N'LS_Designer_WPF.Model.AN6UControlChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params IP = "2.0.0.2"  ChNum = "0"  Port = "0"/>', 1, 1, 1, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (2, N'Universe_1', 1, 3, 1, N'LS_Designer_WPF.Model.AN6UControlChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params IP = "2.0.0.2"  ChNum = "1"  Port = "1"/>', 1, 1, 1, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (3, N'Universe_2', 2, 3, 1, N'LS_Designer_WPF.Model.AN6UControlChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params IP = "2.0.0.2"  ChNum = "2"  Port = "2"/>', 1, 1, 1, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (4, N'Universe_3', 3, 3, 1, N'LS_Designer_WPF.Model.AN6UControlChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params IP = "2.0.0.2"  ChNum = "3"  Port = "3"/>', 1, 1, 1, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (5, N'Universe_4', 4, 0, 1, N'LS_Designer_WPF.Model.AN6UControlChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params IP = "2.0.0.3"  ChNum = "4"  Port = "4"/>', 1, 1, 1, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (6, N'Universe_5', 5, 0, 1, N'LS_Designer_WPF.Model.AN6UControlChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params IP = "2.0.0.3"  ChNum = "5"  Port = "5"/>', 1, 1, 1, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (7, N'NL_W_Ch_0', 0, 0, 1, N'LS_Designer_WPF.Model.NLPowerChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params HaveDimmer="True" PointType="W" />', 0, 3, 2, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (8, N'NL_W_Ch_1', 1, 0, 1, N'LS_Designer_WPF.Model.NLPowerChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params HaveDimmer="True" PointType="W" />', 0, 3, 3, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (9, N'NL_W_Ch_2', 2, 0, 1, N'LS_Designer_WPF.Model.NLPowerChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params HaveDimmer="True" PointType="W" />', 0, 3, 4, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (10, N'NL_W_Ch_3', 3, 0, 1, N'LS_Designer_WPF.Model.NLPowerChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params HaveDimmer="True" PointType="W" />', 0, 3, 5, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (11, N'NL_RGB_Ch_0', 0, 3, 1, N'LS_Designer_WPF.Model.NLPowerChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params HaveDimmer="True" PointType="RGB" />', 0, 3, 6, 1)
SET IDENTITY_INSERT [dbo].[ControlChannels] OFF
SET IDENTITY_INSERT [dbo].[LightElements] ON 

INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (1, N'AN_RGB_8', 3, 1, 120, 0, N'GRB', N'Левый верх', NULL, 3, 1, 1, NULL)
INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (2, N'AN_RGB_1', 3, 26, 144, 1, N'GRB', NULL, NULL, 2, 1, 1, NULL)
INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (3, N'AN_RGB_6', 3, 1, 84, 0, N'GRB', NULL, NULL, 1, 1, 1, NULL)
INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (4, N'AN_RGB_7', 3, 85, 84, 1, N'RGB', NULL, NULL, 1, 1, 1, NULL)
INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (5, N'AN_RGB_2', 3, 1, 25, 1, N'RGB', NULL, NULL, 2, 1, 1, NULL)
INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (6, N'AN_RGB_3', 3, 141, 25, 0, N'RGB', NULL, NULL, 4, 1, 1, NULL)
INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (7, N'AN_RGB_4', 3, 51, 90, 1, N'RGB', NULL, NULL, 4, 1, 1, NULL)
INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (8, N'AN_RGB_5', 3, 1, 50, 1, N'RGB', NULL, NULL, 4, 1, 1, NULL)
INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (9, N'NL_W_1', 0, 1, 1, 0, NULL, N'Потолок1', NULL, 7, 1, 3, NULL)
INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (10, N'NL_W_2', 0, 1, 1, 0, NULL, N'Потолок2', NULL, 8, 1, 3, NULL)
INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (11, N'NL_W_3', 0, 1, 1, 0, NULL, N'Потолок3', NULL, 9, 1, 3, NULL)
INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (12, N'NL_W_4', 0, 1, 1, 0, NULL, N'Стол', NULL, 10, 1, 3, NULL)
INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (13, N'NL_RGB_1', 3, 1, 1, 0, NULL, N'Мойка', NULL, 11, 1, 3, NULL)
SET IDENTITY_INSERT [dbo].[LightElements] OFF
SET IDENTITY_INSERT [dbo].[LightZones] ON 

INSERT [dbo].[LightZones] ([Id], [Name], [IsNode], [PointType], [Remark], [Partition_Id], [ControlSpace_Id]) VALUES (1, N'Левый верх', 0, 3, NULL, 1, 1)
INSERT [dbo].[LightZones] ([Id], [Name], [IsNode], [PointType], [Remark], [Partition_Id], [ControlSpace_Id]) VALUES (2, N'Левый низ', 0, 3, NULL, 1, 1)
INSERT [dbo].[LightZones] ([Id], [Name], [IsNode], [PointType], [Remark], [Partition_Id], [ControlSpace_Id]) VALUES (3, N'Центр верх', 0, 3, NULL, 1, 1)
INSERT [dbo].[LightZones] ([Id], [Name], [IsNode], [PointType], [Remark], [Partition_Id], [ControlSpace_Id]) VALUES (4, N'Подсветка плиты', 0, 3, NULL, 1, 1)
INSERT [dbo].[LightZones] ([Id], [Name], [IsNode], [PointType], [Remark], [Partition_Id], [ControlSpace_Id]) VALUES (5, N'Центр', 0, 3, NULL, 1, 1)
INSERT [dbo].[LightZones] ([Id], [Name], [IsNode], [PointType], [Remark], [Partition_Id], [ControlSpace_Id]) VALUES (6, N'Центр низ Л', 0, 3, NULL, 1, 1)
INSERT [dbo].[LightZones] ([Id], [Name], [IsNode], [PointType], [Remark], [Partition_Id], [ControlSpace_Id]) VALUES (7, N'Центр низ Ц', 0, 3, NULL, 1, 1)
INSERT [dbo].[LightZones] ([Id], [Name], [IsNode], [PointType], [Remark], [Partition_Id], [ControlSpace_Id]) VALUES (8, N'Центр низ П', 0, 3, NULL, 1, 1)
INSERT [dbo].[LightZones] ([Id], [Name], [IsNode], [PointType], [Remark], [Partition_Id], [ControlSpace_Id]) VALUES (9, N'Правый низ', 0, 3, NULL, 1, 1)
INSERT [dbo].[LightZones] ([Id], [Name], [IsNode], [PointType], [Remark], [Partition_Id], [ControlSpace_Id]) VALUES (10, N'Низ', 0, 3, NULL, 1, 1)
INSERT [dbo].[LightZones] ([Id], [Name], [IsNode], [PointType], [Remark], [Partition_Id], [ControlSpace_Id]) VALUES (14, N'Потолок', 0, 0, NULL, 1, 3)
INSERT [dbo].[LightZones] ([Id], [Name], [IsNode], [PointType], [Remark], [Partition_Id], [ControlSpace_Id]) VALUES (16, N'Подсветка мойки', 0, 3, NULL, 1, 3)
SET IDENTITY_INSERT [dbo].[LightZones] OFF
SET IDENTITY_INSERT [dbo].[EventDevices] ON 

INSERT [dbo].[EventDevices] ([Id], [Name], [Model], [Mode], [Profile], [Remark], [MultiChannel], [CanAddChannel], [DotNetType], [ControlSpace_Id], [Partition_Id]) VALUES (1, N'NL_EDev1', N'PB411', 1, N'<Params>
  
  <Mode Value="0">
    <Channel ChannelNo="0">
      <Event Name="StartDimIn"/>
      <Event Name="StopRegulation"/>
      <Event Name="On"/>
    </Channel>
    <Channel ChannelNo="1">
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
      <Event Name="Off"/>
    </Channel>
    <Channel ChannelNo="2">
      <Event Name="StartDimIn"/>
      <Event Name="StopRegulation"/>
      <Event Name="On"/>
    </Channel>
    <Channel ChannelNo="3">
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
      <Event Name="Off"/>
    </Channel>
  </Mode>
  
  <Mode Value="1">
    <Channel ChannelNo="0">
      <Event Name="On"/>
      <Event Name="Off"/>
      <Event Name="StartDimIn"/>
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
    </Channel>
    <Channel ChannelNo="1">
      <Event Name="On"/>
      <Event Name="Off"/>
      <Event Name="StartDimIn"/>
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
    </Channel>
    <Channel ChannelNo="2">
      <Event Name="On"/>
      <Event Name="Off"/>
      <Event Name="StartDimIn"/>
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
    </Channel>
    <Channel ChannelNo="3">
      <Event Name="On"/>
      <Event Name="Off"/>
      <Event Name="StartDimIn"/>
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
    </Channel>
  </Mode>
  
  <Mode Value="2">
    <Channel ChannelNo="0">
      <Event Name="On"/>
      <Event Name="Off"/>
      <Event Name="StartDimIn"/>
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
    </Channel>
    <Channel ChannelNo="1">
      <Event Name="Script"/>
    </Channel>
    <Channel ChannelNo="2">
      <Event Name="On"/>
      <Event Name="Off"/>
      <Event Name="StartDimIn"/>
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
    </Channel>
    <Channel ChannelNo="3">
      <Event Name="Script"/>
    </Channel>
  </Mode>
  
  <Mode Value="3">
    <Channel ChannelNo="0">
      <Event Name="Script"/>
    </Channel>
    <Channel ChannelNo="1">
      <Event Name="Script"/>
    </Channel>
    <Channel ChannelNo="2">
      <Event Name="Script"/>
    </Channel>
    <Channel ChannelNo="3">
      <Event Name="Script"/>
    </Channel>
  </Mode>
</Params>', NULL, 1, 0, N'LS_Designer_WPF.Model.NLEventDevice, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', 3, 1)
SET IDENTITY_INSERT [dbo].[EventDevices] OFF
SET IDENTITY_INSERT [dbo].[EventChannels] ON 

INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (1, N'[0] On', 0, N'On', N'', 3, 1, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (2, N'[0] Off', 0, N'Off', N'', 3, 1, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (3, N'[0] StartDimIn', 0, N'StartDimIn', N'', 3, 1, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (4, N'[0] StartDimOut', 0, N'StartDimOut', N'', 3, 1, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (5, N'[0] StopRegulation', 0, N'StopRegulation', N'', 3, 1, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (6, N'[1] On', 1, N'On', N'', 3, 1, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (7, N'[1] Off', 1, N'Off', N'', 3, 1, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (8, N'[1] StartDimIn', 1, N'StartDimIn', N'', 3, 1, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (9, N'[1] StartDimOut', 1, N'StartDimOut', N'', 3, 1, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (10, N'[1] StopRegulation', 1, N'StopRegulation', N'', 3, 1, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (11, N'[2] On', 2, N'On', N'', 3, 1, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (12, N'[2] Off', 2, N'Off', N'', 3, 1, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (13, N'[2] StartDimIn', 2, N'StartDimIn', N'', 3, 1, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (14, N'[2] StartDimOut', 2, N'StartDimOut', N'', 3, 1, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (15, N'[2] StopRegulation', 2, N'StopRegulation', N'', 3, 1, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (16, N'[3] On', 3, N'On', N'', 3, 1, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (17, N'[3] Off', 3, N'Off', N'', 3, 1, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (18, N'[3] StartDimIn', 3, N'StartDimIn', N'', 3, 1, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (19, N'[3] StartDimOut', 3, N'StartDimOut', N'', 3, 1, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (20, N'[3] StopRegulation', 3, N'StopRegulation', N'', 3, 1, 1)
SET IDENTITY_INSERT [dbo].[EventChannels] OFF
SET IDENTITY_INSERT [dbo].[LE_Types] ON 

INSERT [dbo].[LE_Types] ([Id], [Name], [PointType], [IsActive], [Remark], [CanUseGamma], [ControlSpace_Id]) VALUES (1, N'W', 0, 1, NULL, NULL, 3)
INSERT [dbo].[LE_Types] ([Id], [Name], [PointType], [IsActive], [Remark], [CanUseGamma], [ControlSpace_Id]) VALUES (2, N'RGB', 3, 1, NULL, NULL, 3)
INSERT [dbo].[LE_Types] ([Id], [Name], [PointType], [IsActive], [Remark], [CanUseGamma], [ControlSpace_Id]) VALUES (3, N'W', 0, 1, NULL, NULL, 1)
INSERT [dbo].[LE_Types] ([Id], [Name], [PointType], [IsActive], [Remark], [CanUseGamma], [ControlSpace_Id]) VALUES (4, N'RGB', 3, 1, NULL, NULL, 1)
INSERT [dbo].[LE_Types] ([Id], [Name], [PointType], [IsActive], [Remark], [CanUseGamma], [ControlSpace_Id]) VALUES (5, N'RGBW', 5, 1, NULL, NULL, 1)
SET IDENTITY_INSERT [dbo].[LE_Types] OFF
SET IDENTITY_INSERT [dbo].[EnvironmentItems] ON 

INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (1, N'AN6USPI', 1, N'<Params>
  <IPAddress Value="2.0.0.2" ChCount="4"/>
  <VirtualIP Value="2.0.0.3" ChCount="2"/>
</Params>', N'LS_Designer_WPF.Model.AN6USPI, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null')
INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (2, N'SU111-300', 1, N'<Params Name="NL_PB" HaveDimmer="true" PointType="W"/>', N'LS_Designer_WPF.Model.NLPowerBlock, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null')
INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (3, N'SU111-500', 1, N'<Params Name="NL_PB" HaveDimmer="true" PointType="W"/>', N'LS_Designer_WPF.Model.NLPowerBlock, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null')
INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (4, N'PB411', 0, N'<Params>
  
  <Mode Value="0">
    <Channel ChannelNo="0">
      <Event Name="StartDimIn"/>
      <Event Name="StopRegulation"/>
      <Event Name="On"/>
    </Channel>
    <Channel ChannelNo="1">
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
      <Event Name="Off"/>
    </Channel>
    <Channel ChannelNo="2">
      <Event Name="StartDimIn"/>
      <Event Name="StopRegulation"/>
      <Event Name="On"/>
    </Channel>
    <Channel ChannelNo="3">
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
      <Event Name="Off"/>
    </Channel>
  </Mode>
  
  <Mode Value="1">
    <Channel ChannelNo="0">
      <Event Name="On"/>
      <Event Name="Off"/>
      <Event Name="StartDimIn"/>
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
    </Channel>
    <Channel ChannelNo="1">
      <Event Name="On"/>
      <Event Name="Off"/>
      <Event Name="StartDimIn"/>
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
    </Channel>
    <Channel ChannelNo="2">
      <Event Name="On"/>
      <Event Name="Off"/>
      <Event Name="StartDimIn"/>
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
    </Channel>
    <Channel ChannelNo="3">
      <Event Name="On"/>
      <Event Name="Off"/>
      <Event Name="StartDimIn"/>
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
    </Channel>
  </Mode>
  
  <Mode Value="2">
    <Channel ChannelNo="0">
      <Event Name="On"/>
      <Event Name="Off"/>
      <Event Name="StartDimIn"/>
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
    </Channel>
    <Channel ChannelNo="1">
      <Event Name="Script"/>
    </Channel>
    <Channel ChannelNo="2">
      <Event Name="On"/>
      <Event Name="Off"/>
      <Event Name="StartDimIn"/>
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
    </Channel>
    <Channel ChannelNo="3">
      <Event Name="Script"/>
    </Channel>
  </Mode>
  
  <Mode Value="3">
    <Channel ChannelNo="0">
      <Event Name="Script"/>
    </Channel>
    <Channel ChannelNo="1">
      <Event Name="Script"/>
    </Channel>
    <Channel ChannelNo="2">
      <Event Name="Script"/>
    </Channel>
    <Channel ChannelNo="3">
      <Event Name="Script"/>
    </Channel>
  </Mode>
</Params>', N'LS_Designer_WPF.Model.NLEventDevice, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null')
INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (5, N'SD111-180', 1, N'<Params Name="NL_PBrgb" HaveDimmer="true" PointType="RGB"/>', N'LS_Designer_WPF.Model.NLPowerBlock, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null')
INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (6, N'PB211', 0, N'<Params>
  
  <Mode Value="0">
    <Channel ChannelNo="0">
      <Event Name="StartDimIn"/>
      <Event Name="StopRegulation"/>
      <Event Name="On"/>
    </Channel>
    <Channel ChannelNo="1">
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
      <Event Name="Off"/>
    </Channel>
  </Mode>
  
  <Mode Value="1">
    <Channel ChannelNo="0">
      <Event Name="On"/>
      <Event Name="Off"/>
      <Event Name="StartDimIn"/>
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
    </Channel>
    <Channel ChannelNo="1">
      <Event Name="On"/>
      <Event Name="Off"/>
      <Event Name="StartDimIn"/>
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
    </Channel>
  </Mode>
  
  <Mode Value="2">
    <Channel ChannelNo="0">
      <Event Name="On"/>
      <Event Name="Off"/>
      <Event Name="StartDimIn"/>
      <Event Name="StartDimOut"/>
      <Event Name="StopRegulation"/>
    </Channel>
    <Channel ChannelNo="1">
      <Event Name="Script"/>
    </Channel>
  </Mode>
  
  <Mode Value="3">
    <Channel ChannelNo="0">
      <Event Name="Script"/>
    </Channel>
    <Channel ChannelNo="1">
      <Event Name="Script"/>
    </Channel>
  </Mode>
</Params>', N'LS_Designer_WPF.Model.NLEventDevice, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null')
SET IDENTITY_INSERT [dbo].[EnvironmentItems] OFF
SET IDENTITY_INSERT [dbo].[CSEnvItems] ON 

INSERT [dbo].[CSEnvItems] ([Id], [EnvironmentItem_Id], [ControlSpace_Id]) VALUES (1, 1, 1)
INSERT [dbo].[CSEnvItems] ([Id], [EnvironmentItem_Id], [ControlSpace_Id]) VALUES (2, 2, 3)
INSERT [dbo].[CSEnvItems] ([Id], [EnvironmentItem_Id], [ControlSpace_Id]) VALUES (3, 3, 3)
INSERT [dbo].[CSEnvItems] ([Id], [EnvironmentItem_Id], [ControlSpace_Id]) VALUES (4, 4, 3)
INSERT [dbo].[CSEnvItems] ([Id], [EnvironmentItem_Id], [ControlSpace_Id]) VALUES (5, 5, 3)
INSERT [dbo].[CSEnvItems] ([Id], [EnvironmentItem_Id], [ControlSpace_Id]) VALUES (7, 6, 3)
SET IDENTITY_INSERT [dbo].[CSEnvItems] OFF
SET IDENTITY_INSERT [dbo].[LE_Proxies] ON 

INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (1, 0, 1, 1)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (2, 0, 2, 2)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (3, 0, 3, 3)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (4, 0, 4, 4)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (5, 0, 5, 3)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (6, 1, 5, 4)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (7, 0, 6, 5)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (8, 1, 6, 6)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (9, 0, 7, 7)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (10, 0, 8, 8)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (11, 0, 9, 5)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (14, 3, 9, 8)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (16, 0, 10, 5)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (17, 1, 10, 6)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (18, 2, 10, 7)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (19, 3, 10, 8)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (20, 0, 14, 9)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (21, 1, 14, 10)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (22, 2, 14, 11)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (26, 0, 16, 13)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (33, 1, 9, 6)
INSERT [dbo].[LE_Proxies] ([Id], [Ix], [LightZone_Id], [LightElement_Id]) VALUES (38, 2, 9, 7)
SET IDENTITY_INSERT [dbo].[LE_Proxies] OFF
SET IDENTITY_INSERT [dbo].[Scenes] ON 

INSERT [dbo].[Scenes] ([Id], [Name], [Remark], [Partition_Id], [Parent_Id]) VALUES (3, N'Сцена 1', NULL, 1, NULL)
SET IDENTITY_INSERT [dbo].[Scenes] OFF
