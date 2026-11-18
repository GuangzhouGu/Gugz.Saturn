# HYC.HTCommunication

## **目录**

- [Socket](#Socket)
  - [HTCOMMSocketClient](#HTCOMMSocketClient)
  - [HTCOMMSocketServer](#HTCOMMSocketServer)
  - [HTCOMMUdpClient](#HTCOMMUdpClient)
- [SerialPort](#SerialPort)
  - [HTCOMMSerialPort](#HTCOMMSerialPort)

## Socket

### HTCOMMSocketClient

Socket客户端类

|属性/方法|功能|
|----|-----|
|string ServerIP|服务端IP地址|
|int ServerPort|服务器端口|
|bool Connected|Socket连接状态|
|void Connect(string ipAddr, int port)|初始化Socket连接|
|void Connect()|初始化Socket连接，使用属性ServerIP和ServerPort设置连接信息|
|int Send(byte[] data)|发送数据|
|async Task&lt;int> SendAsync(byte[] data)|异步发送数据|
|void Disconnect()|断开连接|
|void Dispose()|IDispose成员，断开连接|
|event EventHandler&lt;SocketEventArgs> ClientConnected|连接成功事件|
|event EventHandler&lt;SocketEventArgs> ClientDisconnected|连接断开事件|
|event EventHandler&lt;SocketEventArgs> DataReceived|接收到数据事件|

### HTCOMMSocketServer

Socket服务端类

|属性/方法|功能|
|----|-----|
|void Listen(int port, string ip = "")|初始化SocketServer，监听port，不传入IP地址时，默认监听本机所有IP，可指定监听的IP地址|
|List&lt;HTSocketConnection> GetConnections()|获取所有Socket连接|
|List&lt;HTSocketConnection> GetConnections(string remoteIP)|获取来自remoteIP的所有Socket连接|
|HTSocketConnection GetConnection(string remoteIP, int remotePort)|获取指定的IP和Port的Socket连接|
|HTSocketConnection GetConnection(EndPoint remoteEndPoint)|获取指定的EndPoint的Socket连接|
|int Send(string remoteIP, int remotePort, byte[] data)|发送数据|
|int Send(EndPoint ep, byte[] data)|发送数据|
|void Dispose()|断开连接，释放资源|
|event EventHandler&lt;SocketEventArgs> ClientConnected|客户端连接成功事件|
|event EventHandler&lt;SocketEventArgs> ClientDisconnected|客户端连接断开事件|
|event EventHandler&lt;SocketEventArgs> DataReceived|接收到数据事件|

### HTCOMMUdpClient

Udp客户端

|属性/方法|功能|
|----|-----|
|void Listen(int port, string ip = "")|初始化SocketServer，监听port，不传入IP地址时，默认监听本机所有IP，可指定监听的IP地址|
|int Send(int remotePort, byte[] data)|发送数据（广播）|
|int Send(string remoteIP, int remotePort, byte[] data)|发送数据|
|void Dispose()|断开连接，释放资源|
|event EventHandler&lt;SocketEventArgs> DataReceived|接收到数据事件|

## SerialPort

### HTCOMMSerialPort

串口通信类

|属性/方法|功能|
|----|-----|
|bool IsOpen|端口打开状态|
|bool Init(HTSerialPortInfo info)|初始化串口|
|bool Open()|使用已经传入的参数重新初始化串口|
|void Send(byte[] data)|发送数据|
|void Send(string text)|发送文本信息|
|bool Close()|关闭串口|
|void Dispose()|释放串口连接和资源|
|event EventHandler&lt;SocketEventArgs> DataReceived|接收到数据事件|
