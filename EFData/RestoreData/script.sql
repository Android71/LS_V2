USE [LightSystemV1]
GO
SET IDENTITY_INSERT [dbo].[ControlSpaces] ON 

INSERT [dbo].[ControlSpaces] ([Id], [Name], [IsActive]) VALUES (1, N'ArtNet', 1)
INSERT [dbo].[ControlSpaces] ([Id], [Name], [IsActive]) VALUES (2, N'DMX', 0)
INSERT [dbo].[ControlSpaces] ([Id], [Name], [IsActive]) VALUES (3, N'NooLite', 1)
INSERT [dbo].[ControlSpaces] ([Id], [Name], [IsActive]) VALUES (4, N'MiLight', 0)
SET IDENTITY_INSERT [dbo].[ControlSpaces] OFF
SET IDENTITY_INSERT [dbo].[Partitions] ON 

INSERT [dbo].[Partitions] ([Id], [Name]) VALUES (1, N'Кухня')
INSERT [dbo].[Partitions] ([Id], [Name]) VALUES (2, N'Прихожая')
INSERT [dbo].[Partitions] ([Id], [Name]) VALUES (3, N'Детская')
SET IDENTITY_INSERT [dbo].[Partitions] OFF
SET IDENTITY_INSERT [dbo].[EventDevices] ON 

INSERT [dbo].[EventDevices] ([Id], [Name], [Model], [Mode], [Profile], [Remark], [MultiChannel], [CanAddChannel], [DotNetType], [ControlSpace_Id], [Partition_Id]) VALUES (5, N'NL_EDev41', N'PB411', 0, N'<Params>
  
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
INSERT [dbo].[EventDevices] ([Id], [Name], [Model], [Mode], [Profile], [Remark], [MultiChannel], [CanAddChannel], [DotNetType], [ControlSpace_Id], [Partition_Id]) VALUES (6, N'NL_EDev55', N'PB211', 1, N'<Params>
  
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
INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (7, N'SD111-180', 1, N'<Params Name="NL_RGB" HaveDimmer="true" PointType="RGB"/>', N'LS_Designer_WPF.Model.NLPowerBlock, LS_Designer_WPF, Version=0.0.0.1, Culture=neutral, PublicKeyToken=null')
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
