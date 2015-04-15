Public Class Logs
    Public Property m_LogID As Integer
        Get
            Return LogID
        End Get
        Set(value As Integer)
            LogID = value
        End Set
    End Property
    Private LogID As Integer
    Public Property m_SiteID As Integer
        Get
            Return SiteID
        End Get
        Set(value As Integer)
            SiteID = value
        End Set
    End Property
    Private SiteID As String
    Public Property m_DateRequestEnd As DateTime
        Get
            Return DateRequestEnd
        End Get
        Set(value As DateTime)
            DateRequestEnd = value
        End Set
    End Property
    Private DateRequestEnd As DateTime
    Public Property m_ServerIP As String
        Get
            Return ServerIP
        End Get
        Set(value As String)
            ServerIP = value
        End Set
    End Property
    Private ServerIP As String
    Public Property m_RequestMethod As String
        Get
            Return RequestMethod
        End Get
        Set(value As String)
            RequestMethod = value
        End Set
    End Property
    Private RequestMethod As String
    Public Property m_RequestURIBase As String
        Get
            Return RequestURIBase
        End Get
        Set(value As String)
            RequestURIBase = value
        End Set
    End Property
    Private RequestURIBase As String
    Public Property m_RequestQueryString As String
        Get
            Return RequestQueryString
        End Get
        Set(value As String)
            RequestQueryString = value
        End Set
    End Property
    Private RequestQueryString As String
    Public Property m_ServerPort As Integer
        Get
            Return ServerPort
        End Get
        Set(value As Integer)
            ServerPort = value
        End Set
    End Property
    Private ServerPort As Integer
    Public Property m_ClientUserName As String
        Get
            Return ClientUserName
        End Get
        Set(value As String)
            ClientUserName = value
        End Set
    End Property
    Private ClientUserName As String
    Public Property m_ClientIP As String
        Get
            Return ClientIP
        End Get
        Set(value As String)
            ClientIP = value
        End Set
    End Property
    Private ClientIP As String
    Public Property m_ClientUserAgent As String
        Get
            Return ClientUserAgent
        End Get
        Set(value As String)
            ClientUserAgent = value
        End Set
    End Property
    Private ClientUserAgent As String
    Public Property m_HttpStatusCode As String
        Get
            Return HttpStatusCode
        End Get
        Set(value As String)
            HttpStatusCode = value
        End Set
    End Property
    Private HttpStatusCode As String
    Public Property m_HttpSubStatus As Integer
        Get
            Return HttpSubStatus
        End Get
        Set(value As Integer)
            HttpSubStatus = value
        End Set
    End Property
    Private HttpSubStatus As Integer
    Public Property m_WindowsStatusCode As Integer
        Get
            Return WindowsStatusCode
        End Get
        Set(value As Integer)
            WindowsStatusCode = value
        End Set
    End Property
    Private WindowsStatusCode As Integer
    Public Property m_ProcessLength As String
        Get
            Return ProcessLength
        End Get
        Set(value As String)
            ProcessLength = value
        End Set
    End Property
    Private ProcessLength As String
    Public Property m_DateCreated As DateTime
        Get
            Return DateCreated
        End Get
        Set(value As DateTime)
            DateCreated = value
        End Set
    End Property
    Private DateCreated As DateTime
End Class
