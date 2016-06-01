USE [LightSystemV1]
GO
SET IDENTITY_INSERT [dbo].[EnvironmentItems] ON 

INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (2, N'AN6USPI', 2, N'<Params>
  <IPAddress Value="2.0.0.2" ChCount="4"/>
  <VirtualIP Value="2.0.0.3" ChCount="2"/>
</Params>', NULL)
INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (3, N'SU111-300', 1, N'<Params HaveDimmer="true" />', NULL)
INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (4, N'SU111-500', 1, N'<Params HaveDimmer="true" />', NULL)
INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (5, N'PB411', 0, N'<Params>
  <Mode Value="1">
    <EventSource ChannelNo="1">
      <Event Name="DimIn"/>
      <Event Name="On"/>
    </EventSource>
    <EventSource ChannelNo="2">
      <Event Name="DimOut"/>
      <Event Name="Off"/>
    </EventSource>
    <EventSource ChannelNo="3">
      <Event Name="DimIn"/>
      <Event Name="On"/>
    </EventSource>
    <EventSource ChannelNo="4">
      <Event Name="DimOut"/>
      <Event Name="Off"/>
    </EventSource>
  </Mode>
  <Mode Value="2">
    <EventSource ChannelNo="1">
      <Event Name="On/Off Trigger"/>
      <Event Name="Dim In/Out"/>
    </EventSource>
    <EventSource ChannelNo="2">
      <Event Name="On/Off Trigger"/>
      <Event Name="Dim In/Out"/>
    </EventSource>
    <EventSource ChannelNo="3">
      <Event Name="On/Off Trigger"/>
      <Event Name="Dim In/Out"/>
    </EventSource>
    <EventSource ChannelNo="4">
      <Event Name="On/Off Trigger"/>
      <Event Name="Dim In/Out"/>
    </EventSource>
  </Mode>
  <Mode Value="3">
    <EventSource ChannelNo="1">
      <Event Name="Dim In/Out"/>
      <Event Name="On"/>
    </EventSource>
    <EventSource ChannelNo="2">
      <Event Name="On"/>
    </EventSource>
    <EventSource ChannelNo="3">
      <Event Name="Dim In/Out"/>
      <Event Name="On"/>
    </EventSource>
    <EventSource ChannelNo="4">
      <Event Name="On"/>
    </EventSource>
  </Mode>
  <Mode Value="4">
    <EventSource ChannelNo="1">
      <Event Name="On"/>
    </EventSource>
    <EventSource ChannelNo="2">
      <Event Name="On"/>
    </EventSource>
    <EventSource ChannelNo="3">
      <Event Name="On"/>
    </EventSource>
    <EventSource ChannelNo="4">
      <Event Name="On"/>
    </EventSource>
  </Mode>
</Params>', NULL)
INSERT [dbo].[EnvironmentItems] ([Id], [Model], [DeviceType], [Profile], [DotNetType]) VALUES (6, N'SD111-180', 1, N'<Params HaveDimmer="true" />', NULL)
SET IDENTITY_INSERT [dbo].[EnvironmentItems] OFF
SET IDENTITY_INSERT [dbo].[ControlSpaces] ON 

INSERT [dbo].[ControlSpaces] ([Id], [Name], [IsActive], [EnvironmentItem_Id]) VALUES (1, N'ArtNet_DMX', 1, 2)
INSERT [dbo].[ControlSpaces] ([Id], [Name], [IsActive], [EnvironmentItem_Id]) VALUES (2, N'NooLite', 1, 3)
INSERT [dbo].[ControlSpaces] ([Id], [Name], [IsActive], [EnvironmentItem_Id]) VALUES (3, N'NooLite', 1, 4)
INSERT [dbo].[ControlSpaces] ([Id], [Name], [IsActive], [EnvironmentItem_Id]) VALUES (4, N'NooLite', 1, 5)
INSERT [dbo].[ControlSpaces] ([Id], [Name], [IsActive], [EnvironmentItem_Id]) VALUES (5, N'NooLite', 1, 6)
SET IDENTITY_INSERT [dbo].[ControlSpaces] OFF
