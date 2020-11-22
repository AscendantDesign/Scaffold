-- NodeData.sql
-- Individual nodes for direct access.
DROP TABLE IF EXISTS NodeData;
CREATE TABLE NodeData
(
	NodeDataTicket TEXT,
	ConversationTicket TEXT,
	NodeItemTicket TEXT,
	NodeText TEXT,
	NodeDelay INT DEFAULT 0,
	NodeImageUrl TEXT
);
