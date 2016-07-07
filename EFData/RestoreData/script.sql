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
INSERT [dbo].[Partitions] ([Id], [Name]) VALUES (2, N'Прихожая')
INSERT [dbo].[Partitions] ([Id], [Name]) VALUES (3, N'Детская')
SET IDENTITY_INSERT [dbo].[Partitions] OFF
SET IDENTITY_INSERT [dbo].[ControlDevices] ON 

INSERT [dbo].[ControlDevices] ([Id], [Name], [Model], [HaveDimmer], [Profile], [Remark], [MultiChannel], [CanAddChannel], [DotNetType], [ControlSpace_Id], [Partition_Id]) VALUES (1, N'AN_Dev1', N'AN6USPI', 1, N'<Params>
  <IPAddress Value="2.0.0.2" />
  <VirtualIP Value="2.0.0.3" />
</Params>', NULL, 1, 0, N'LS_Designer_WPF.Model.AN6USPI, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', 1, 1)
INSERT [dbo].[ControlDevices] ([Id], [Name], [Model], [HaveDimmer], [Profile], [Remark], [MultiChannel], [CanAddChannel], [DotNetType], [ControlSpace_Id], [Partition_Id]) VALUES (2, N'AN_Dev2', N'AN6USPI', 1, N'<Params>
  <IPAddress Value="2.0.0.6" />
  <VirtualIP Value="2.0.0.7" />
</Params>', NULL, 1, 0, N'LS_Designer_WPF.Model.AN6USPI, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', 1, 1)
INSERT [dbo].[ControlDevices] ([Id], [Name], [Model], [HaveDimmer], [Profile], [Remark], [MultiChannel], [CanAddChannel], [DotNetType], [ControlSpace_Id], [Partition_Id]) VALUES (5, N'NL_PB1', N'SU111-500', 1, N'<Params Model="SU111-500" HaveDimmer="True" PointType="W" />', NULL, 0, 0, N'LS_Designer_WPF.Model.NLPowerBlock, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', 3, 1)
INSERT [dbo].[ControlDevices] ([Id], [Name], [Model], [HaveDimmer], [Profile], [Remark], [MultiChannel], [CanAddChannel], [DotNetType], [ControlSpace_Id], [Partition_Id]) VALUES (6, N'NL_PBrgb1', N'SD111-180', 1, N'<Params Model="SD111-180" HaveDimmer="True" PointType="RGB" />', NULL, 0, 0, N'LS_Designer_WPF.Model.NLPowerBlock, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', 3, 1)
INSERT [dbo].[ControlDevices] ([Id], [Name], [Model], [HaveDimmer], [Profile], [Remark], [MultiChannel], [CanAddChannel], [DotNetType], [ControlSpace_Id], [Partition_Id]) VALUES (7, N'NL_PBrgb2', N'SD111-180', 1, N'<Params Model="SD111-180" HaveDimmer="True" PointType="RGB" />', NULL, 0, 0, N'LS_Designer_WPF.Model.NLPowerBlock, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', 3, 1)
SET IDENTITY_INSERT [dbo].[ControlDevices] OFF
SET IDENTITY_INSERT [dbo].[ControlChannels] ON 

INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (1, N'Universe_0', 0, 3, 1, N'LS_Designer_WPF.Model.AN6UControlChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params IP = "2.0.0.2"  ChNum = "0"  Port = "0"/>', 1, 1, 1, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (2, N'Universe_1', 1, 3, 1, N'LS_Designer_WPF.Model.AN6UControlChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params IP = "2.0.0.2"  ChNum = "1"  Port = "1"/>', 1, 1, 1, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (3, N'Universe_2', 2, 3, 1, N'LS_Designer_WPF.Model.AN6UControlChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params IP = "2.0.0.2"  ChNum = "2"  Port = "2"/>', 1, 1, 1, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (4, N'Universe_3', 3, 3, 1, N'LS_Designer_WPF.Model.AN6UControlChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params IP = "2.0.0.2"  ChNum = "3"  Port = "3"/>', 1, 1, 1, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (5, N'Universe_4', 4, 3, 1, N'LS_Designer_WPF.Model.AN6UControlChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params IP = "2.0.0.3"  ChNum = "4"  Port = "4"/>', 1, 1, 1, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (6, N'Universe_5', 5, 0, 1, N'LS_Designer_WPF.Model.AN6UControlChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params IP = "2.0.0.3"  ChNum = "5"  Port = "5"/>', 1, 1, 1, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (7, N'Universe_6', 6, 0, 1, N'LS_Designer_WPF.Model.AN6UControlChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params IP = "2.0.0.6"  ChNum = "6"  Port = "0"/>', 1, 1, 2, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (8, N'Universe_7', 7, 0, 1, N'LS_Designer_WPF.Model.AN6UControlChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params IP = "2.0.0.6"  ChNum = "7"  Port = "1"/>', 1, 1, 2, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (9, N'Universe_8', 8, 0, 1, N'LS_Designer_WPF.Model.AN6UControlChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params IP = "2.0.0.6"  ChNum = "8"  Port = "2"/>', 1, 1, 2, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (10, N'Universe_9', 9, 0, 1, N'LS_Designer_WPF.Model.AN6UControlChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params IP = "2.0.0.6"  ChNum = "9"  Port = "3"/>', 1, 1, 2, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (11, N'Universe_10', 10, 0, 1, N'LS_Designer_WPF.Model.AN6UControlChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params IP = "2.0.0.7"  ChNum = "10"  Port = "9"/>', 1, 1, 2, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (12, N'Universe_11', 11, 0, 1, N'LS_Designer_WPF.Model.AN6UControlChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params IP = "2.0.0.7"  ChNum = "11"  Port = "5"/>', 1, 1, 2, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (15, N'NL_W_Ch_0', 0, 0, 1, N'LS_Designer_WPF.Model.NLPowerChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params HaveDimmer="True" PointType="W" />', 0, 3, 5, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (16, N'NL_RGB_Ch_1', 1, 3, 1, N'LS_Designer_WPF.Model.NLPowerChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params HaveDimmer="True" PointType="RGB" />', 0, 3, 6, 1)
INSERT [dbo].[ControlChannels] ([Id], [Name], [ChannelNo], [PointType], [HaveDimmer], [DotNetType], [Profile], [Multilink], [ControlSpace_Id], [ControlDevice_Id], [Partition_Id]) VALUES (17, N'NL_RGB_Ch_3', 3, 3, 1, N'LS_Designer_WPF.Model.NLPowerChannel, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', N'<Params HaveDimmer="True" PointType="RGB" />', 0, 3, 7, 1)
SET IDENTITY_INSERT [dbo].[ControlChannels] OFF
SET IDENTITY_INSERT [dbo].[LightElements] ON 

INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (5, N'NL_LE_6', 0, 1, 1, 0, NULL, NULL, NULL, 15, 1, 3, NULL)
INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (6, N'NL_LE_7', 3, 1, 1, 0, NULL, N'Мойка', NULL, 17, 1, 3, NULL)
INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (20, N'AN_RGB_3', 3, 140, 31, 0, N'RGB', NULL, NULL, 2, 1, 1, NULL)
INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (22, N'AN_RGB_4', 3, 51, 89, 1, N'RGB', NULL, NULL, 3, 1, 1, NULL)
INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (23, N'AN_RGB_5', 3, 1, 50, 1, N'RGB', NULL, NULL, NULL, 1, 1, NULL)
INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (1023, N'AN_RGB_1', 3, 26, 144, 1, N'RGB', NULL, NULL, NULL, 1, 1, NULL)
INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (1024, N'AN_RGB_2', 3, 1, 25, 1, N'RGB', NULL, NULL, NULL, 1, 1, NULL)
INSERT [dbo].[LightElements] ([Id], [Name], [PointType], [StartPoint], [PointCount], [Direction], [ColorSequence], [Remark], [Gamma_Id], [ControlChannel_Id], [Partition_Id], [ControlSpace_Id], [CustomGamma_Id]) VALUES (1025, N'AN_RGB_6', 3, 1, 84, 0, N'RGB', NULL, NULL, NULL, 1, 1, NULL)
SET IDENTITY_INSERT [dbo].[LightElements] OFF
SET IDENTITY_INSERT [dbo].[EventDevices] ON 

INSERT [dbo].[EventDevices] ([Id], [Name], [Model], [Mode], [Profile], [Remark], [MultiChannel], [CanAddChannel], [DotNetType], [ControlSpace_Id], [Partition_Id]) VALUES (26, N'NL_EDev1', N'PB411', 0, N'<Params>
  <Mode Value="0">
    <Channel ChannelNo="6">
      <Event Name="StartDimIn" />
      <Event Name="StopRegulation" />
      <Event Name="On" />
    </Channel>
    <Channel ChannelNo="7">
      <Event Name="StartDimOut" />
      <Event Name="StopRegulation" />
      <Event Name="Off" />
    </Channel>
    <Channel ChannelNo="2">
      <Event Name="StartDimIn" />
      <Event Name="StopRegulation" />
      <Event Name="On" />
    </Channel>
    <Channel ChannelNo="3">
      <Event Name="StartDimOut" />
      <Event Name="StopRegulation" />
      <Event Name="Off" />
    </Channel>
  </Mode>
  <Mode Value="1">
    <Channel ChannelNo="6">
      <Event Name="On" />
      <Event Name="Off" />
      <Event Name="StartDimIn" />
      <Event Name="StartDimOut" />
      <Event Name="StopRegulation" />
    </Channel>
    <Channel ChannelNo="7">
      <Event Name="On" />
      <Event Name="Off" />
      <Event Name="StartDimIn" />
      <Event Name="StartDimOut" />
      <Event Name="StopRegulation" />
    </Channel>
    <Channel ChannelNo="2">
      <Event Name="On" />
      <Event Name="Off" />
      <Event Name="StartDimIn" />
      <Event Name="StartDimOut" />
      <Event Name="StopRegulation" />
    </Channel>
    <Channel ChannelNo="3">
      <Event Name="On" />
      <Event Name="Off" />
      <Event Name="StartDimIn" />
      <Event Name="StartDimOut" />
      <Event Name="StopRegulation" />
    </Channel>
  </Mode>
  <Mode Value="2">
    <Channel ChannelNo="6">
      <Event Name="On" />
      <Event Name="Off" />
      <Event Name="StartDimIn" />
      <Event Name="StartDimOut" />
      <Event Name="StopRegulation" />
    </Channel>
    <Channel ChannelNo="7">
      <Event Name="Script" />
    </Channel>
    <Channel ChannelNo="2">
      <Event Name="On" />
      <Event Name="Off" />
      <Event Name="StartDimIn" />
      <Event Name="StartDimOut" />
      <Event Name="StopRegulation" />
    </Channel>
    <Channel ChannelNo="3">
      <Event Name="Script" />
    </Channel>
  </Mode>
  <Mode Value="3">
    <Channel ChannelNo="6">
      <Event Name="Script" />
    </Channel>
    <Channel ChannelNo="7">
      <Event Name="Script" />
    </Channel>
    <Channel ChannelNo="2">
      <Event Name="Script" />
    </Channel>
    <Channel ChannelNo="3">
      <Event Name="Script" />
    </Channel>
  </Mode>
</Params>', NULL, 1, 0, N'LS_Designer_WPF.Model.NLEventDevice, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', 3, 1)
INSERT [dbo].[EventDevices] ([Id], [Name], [Model], [Mode], [Profile], [Remark], [MultiChannel], [CanAddChannel], [DotNetType], [ControlSpace_Id], [Partition_Id]) VALUES (27, N'NL_EDev', N'PB211', 3, N'<Params>
  
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
</Params>', NULL, 1, 0, N'LS_Designer_WPF.Model.NLEventDevice, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null', 3, 1)
SET IDENTITY_INSERT [dbo].[EventDevices] OFF
SET IDENTITY_INSERT [dbo].[EventChannels] ON 

INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (355, N'[6] StartDimIn', 6, N'StartDimIn', N'', 3, 26, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (356, N'[6] StopRegulation', 6, N'StopRegulation', N'', 3, 26, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (357, N'[6] On', 6, N'On', N'', 3, 26, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (358, N'[7] StartDimOut', 7, N'StartDimOut', N'', 3, 26, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (359, N'[7] StopRegulation', 7, N'StopRegulation', N'', 3, 26, 2)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (360, N'[7] Off', 7, N'Off', N'', 3, 26, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (361, N'[2] StartDimIn', 2, N'StartDimIn', N'', 3, 26, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (362, N'[2] StopRegulation', 2, N'StopRegulation', N'', 3, 26, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (363, N'[2] On', 2, N'On', N'', 3, 26, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (364, N'[3] StartDimOut', 3, N'StartDimOut', N'', 3, 26, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (365, N'[3] StopRegulation', 3, N'StopRegulation', N'', 3, 26, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (366, N'[3] Off', 3, N'Off', N'', 3, 26, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (367, N'[0] Script', 0, N'Script', N'', 3, 27, 1)
INSERT [dbo].[EventChannels] ([Id], [Name], [ChannelNo], [EventName], [Profile], [ControlSpace_Id], [EventDevice_Id], [Partition_Id]) VALUES (368, N'[1] Script', 1, N'Script', N'', 3, 27, 1)
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
INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (4, N'SU111-500', 1, N'<Params Name="NL_PB" HaveDimmer="true" PointType="W"/>', N'LS_Designer_WPF.Model.NLPowerBlock, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null')
INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (6, N'PB411', 0, N'<Params>
  
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
INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (7, N'SD111-180', 1, N'<Params Name="NL_PBrgb" HaveDimmer="true" PointType="RGB"/>', N'LS_Designer_WPF.Model.NLPowerBlock, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null')
INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (8, N'PB211', 0, N'<Params>
  
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
INSERT [dbo].[CSEnvItems] ([Id], [EnvironmentItem_Id], [ControlSpace_Id]) VALUES (3, 4, 3)
INSERT [dbo].[CSEnvItems] ([Id], [EnvironmentItem_Id], [ControlSpace_Id]) VALUES (4, 6, 3)
INSERT [dbo].[CSEnvItems] ([Id], [EnvironmentItem_Id], [ControlSpace_Id]) VALUES (5, 7, 3)
INSERT [dbo].[CSEnvItems] ([Id], [EnvironmentItem_Id], [ControlSpace_Id]) VALUES (6, 8, 3)
SET IDENTITY_INSERT [dbo].[CSEnvItems] OFF
