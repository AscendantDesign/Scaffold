-- UserItem.sql
-- System Users.
DROP TABLE IF EXISTS UserItem;
CREATE TABLE UserItem
(
	UserItemTicket TEXT,
	SlackID TEXT,
	SlackName TEXT
);
INSERT INTO UserItem(UserItemTicket, SlackID, SlackName) VALUES('588403af-986d-43de-b882-762ad2ca07fe', 'U01EV6ZRM70', 'Daniel');
