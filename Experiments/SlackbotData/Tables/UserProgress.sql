-- UserProgress.sql
-- Course progress for each user.
DROP TABLE IF EXISTS UserProgress;
CREATE TABLE UserProgress
(
	UserProgressTicket TEXT,
	UserItemTicket TEXT,
	ConversationTicket TEXT,
	ConversationState INT DEFAULT 0,
	UserLevel INT DEFAULT 0
);
