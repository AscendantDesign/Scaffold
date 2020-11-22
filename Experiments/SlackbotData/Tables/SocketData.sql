-- SocketData.sql
-- Individual sockets for direct access.
DROP TABLE IF EXISTS SocketData;
CREATE TABLE SocketData
(
	SocketDataTicket TEXT,
	NodeItemTicket TEXT,
	SocketItemTicket TEXT,
	NextNodeTicket TEXT,
	SocketType TEXT,
	SocketText TEXT,
	SocketImageUrl TEXT
);
