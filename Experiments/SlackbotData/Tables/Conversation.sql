-- Conversation.sql
-- Conversational data for chatbot.
SELECT "Delete previous Conversation table.";
DROP TABLE IF EXISTS Conversation;
SELECT "Create new Conversation table.";
CREATE TABLE Conversation
(
	ConversationTicket TEXT,
	ConversationTitle TEXT,
	ConversationDescription TEXT,
	ConversationData
);
