﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ScaffoldSlackPack {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "16.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class ResourceMain {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal ResourceMain() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ScaffoldSlackPack.ResourceMain", typeof(ResourceMain).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (?s:\s*(?&lt;first&gt;[^ \t\r\n]{1}).*$).
        /// </summary>
        internal static string rxCharPatternFirst {
            get {
                return ResourceManager.GetString("rxCharPatternFirst", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (?s:.*?(?&lt;last&gt;[^ \t\r\n]{1})\s*$).
        /// </summary>
        internal static string rxCharPatternLast {
            get {
                return ResourceManager.GetString("rxCharPatternLast", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (?i:(^|\W*)(?&lt;word&gt;[a-z0-9-.,_]+)(\W+|$)).
        /// </summary>
        internal static string rxCommandWordAny {
            get {
                return ResourceManager.GetString("rxCommandWordAny", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (?i:(\W+|^)(?&lt;command&gt;(list|play|start|show|stop|pause|resume))(\W+|$)).
        /// </summary>
        internal static string rxCommandWordCommand {
            get {
                return ResourceManager.GetString("rxCommandWordCommand", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to (?i:(^|\W*)(?&lt;noun&gt;(lesson|lessons|course|courses|module|modules|conversation|conversations|scenario|scenarios))(\W+|$)).
        /// </summary>
        internal static string rxCommandWordNoun {
            get {
                return ResourceManager.GetString("rxCommandWordNoun", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to ^(?&lt;value&gt;-{0,1}[0-9,]*\.{0,1}[0-9]*)$.
        /// </summary>
        internal static string rxNumeric {
            get {
                return ResourceManager.GetString("rxNumeric", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://slack.com/api/users.list.
        /// </summary>
        internal static string SlackAPIUsersList {
            get {
                return ResourceManager.GetString("SlackAPIUsersList", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to https://slack.com/api/chat.postMessage.
        /// </summary>
        internal static string SlackMethodChatPostMessage {
            get {
                return ResourceManager.GetString("SlackMethodChatPostMessage", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM Conversation WHERE ConversationTicket = &apos;{0}&apos;;.
        /// </summary>
        internal static string sqlConversationDelete {
            get {
                return ResourceManager.GetString("sqlConversationDelete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO Conversation(ConversationTicket, ConversationTitle, ConversationDescription)
        ///VALUES
        ///( $ConversationTicket, $ConversationTitle, $ConversationDescription);.
        /// </summary>
        internal static string sqlConversationInsert {
            get {
                return ResourceManager.GetString("sqlConversationInsert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM Conversation WHERE ConversationTicket = &apos;{0}&apos;;.
        /// </summary>
        internal static string sqlConversationSelect {
            get {
                return ResourceManager.GetString("sqlConversationSelect", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT rowid, ConversationTicket, ConversationTitle, ConversationDescription FROM Conversation;.
        /// </summary>
        internal static string sqlConversationSelectCatalogAll {
            get {
                return ResourceManager.GetString("sqlConversationSelectCatalogAll", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT ROWID
        ///FROM Conversation WHERE
        ///ConversationTicket = &apos;{0}&apos;.
        /// </summary>
        internal static string sqlConversationSelectIDForTicket {
            get {
                return ResourceManager.GetString("sqlConversationSelectIDForTicket", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE Conversation SET
        ///ConversationTitle = $ConversationTitle,
        ///ConversationDescription = $ConversationDescription
        ///WHERE
        ///ROWID = {0}.
        /// </summary>
        internal static string sqlConversationUpdate {
            get {
                return ResourceManager.GetString("sqlConversationUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM NodeData WHERE ConversationTicket = &apos;{0}&apos;;.
        /// </summary>
        internal static string sqlNodeDataDeleteConversation {
            get {
                return ResourceManager.GetString("sqlNodeDataDeleteConversation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO NodeData(NodeItemTicket, ConversationTicket, NodeType, NodeText, NodeDelay, NodeImageUrl, NodeLinkUrl) VALUES ($NodeItemTicket, $ConversationTicket, $NodeType, $NodeText, $NodeDelay, $NodeImageUrl, $NodeLinkUrl);.
        /// </summary>
        internal static string sqlNodeDataInsert {
            get {
                return ResourceManager.GetString("sqlNodeDataInsert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM NodeData WHERE NodeItemTicket = &apos;{0}&apos;;.
        /// </summary>
        internal static string sqlNodeDataSelect {
            get {
                return ResourceManager.GetString("sqlNodeDataSelect", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT rowid FROM NodeData WHERE NodeItemTicket = &apos;{0}&apos;;.
        /// </summary>
        internal static string sqlNodeDataSelectID {
            get {
                return ResourceManager.GetString("sqlNodeDataSelectID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM NodeData WHERE ConversationTicket = &apos;{0}&apos; AND NodeType = &apos;Start&apos;;.
        /// </summary>
        internal static string sqlNodeDataSelectStart {
            get {
                return ResourceManager.GetString("sqlNodeDataSelectStart", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE NodeData SET ConversationTicket = $ConversationTicket, NodeType = $NodeType, NodeText = $NodeText, NodeDelay = $NodeDelay, NodeImageUrl = $NodeImageUrl, NodeLinkUrl = $NodeLinkUrl WHERE NodeItemTicket = $NodeItemTicket;.
        /// </summary>
        internal static string sqlNodeDataUpdate {
            get {
                return ResourceManager.GetString("sqlNodeDataUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM SocketData
        ///WHERE NodeItemTicket IN(
        ///	SELECT NodeItemTicket
        ///	FROM NodeData
        ///	WHERE ConversationTicket = &apos;{0}&apos;
        ///);.
        /// </summary>
        internal static string sqlSocketDataDeleteConversation {
            get {
                return ResourceManager.GetString("sqlSocketDataDeleteConversation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO SocketData(SocketItemTicket, NodeItemTicket, NextNodeItemTicket, NextSocketItemTicket, SocketType, SocketText, SocketImageUrl, SocketLinkUrl) VALUES ($SocketItemTicket, $NodeItemTicket, $NextNodeItemTicket, $NextSocketItemTicket, $SocketType, $SocketText, $SocketImageUrl, $SocketLinkUrl);.
        /// </summary>
        internal static string sqlSocketDataInsert {
            get {
                return ResourceManager.GetString("sqlSocketDataInsert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT rowid FROM SocketData WHERE SocketItemTicket = &apos;{0}&apos;;.
        /// </summary>
        internal static string sqlSocketDataSelectID {
            get {
                return ResourceManager.GetString("sqlSocketDataSelectID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM SocketData WHERE NodeItemTicket = &apos;{0}&apos;;.
        /// </summary>
        internal static string sqlSocketDataSelectNode {
            get {
                return ResourceManager.GetString("sqlSocketDataSelectNode", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE SocketData SET NodeItemTicket = $NodeItemTicket, NextNodeItemTicket = $NextNodeItemTicket, NextSocketItemTicket = $NextSocketItemTicket, SocketType = $SocketType, SocketText = $SocketText, SocketImageUrl = $SocketImageUrl, SocketLinkUrl = $SocketLinkUrl WHERE SocketItemTicket = $SocketItemTicket.
        /// </summary>
        internal static string sqlSocketDataUpdate {
            get {
                return ResourceManager.GetString("sqlSocketDataUpdate", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO UserItem(UserItemTicket, SlackID, SlackName) SELECT &apos;{0}&apos;, &apos;{1}&apos;, &apos;{2}&apos; WHERE NOT EXISTS(SELECT 1 FROM UserItem WHERE UserItemTicket = &apos;{0}&apos; OR SlackID = &apos;{1}&apos;);.
        /// </summary>
        internal static string sqlUserItemInsertUnique {
            get {
                return ResourceManager.GetString("sqlUserItemInsertUnique", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT rowid, UserItemTicket, SlackID, SlackName FROM UserItem;.
        /// </summary>
        internal static string sqlUserItemSelectAll {
            get {
                return ResourceManager.GetString("sqlUserItemSelectAll", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT rowid, UserItemTicket, SlackID, SlackName FROM UserItem WHERE SlackID = &apos;{0}&apos;;.
        /// </summary>
        internal static string sqlUserItemSelectSlackID {
            get {
                return ResourceManager.GetString("sqlUserItemSelectSlackID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT UserItemTicket FROM UserItem WHERE SlackID = &apos;{0}&apos;;.
        /// </summary>
        internal static string sqlUserItemSelectTicket {
            get {
                return ResourceManager.GetString("sqlUserItemSelectTicket", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE UserItem SET SlackName = &apos;{0}&apos; WHERE SlackID = &apos;{1}&apos;;.
        /// </summary>
        internal static string sqlUserItemUpdateSlackNameFromSlackID {
            get {
                return ResourceManager.GetString("sqlUserItemUpdateSlackNameFromSlackID", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to DELETE FROM UserProgress WHERE ConversationTicket = &apos;{0}&apos;;.
        /// </summary>
        internal static string sqlUserProgressDeleteConversation {
            get {
                return ResourceManager.GetString("sqlUserProgressDeleteConversation", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to INSERT INTO UserProgress(UserProgressTicket, UserItemTicket, ConversationTicket, ConversationState, UserLevel) VALUES (&apos;{0}&apos;, &apos;{1}&apos;, &apos;{2}&apos;, {3}, {4});.
        /// </summary>
        internal static string sqlUserProgressInsert {
            get {
                return ResourceManager.GetString("sqlUserProgressInsert", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM UserProgress WHERE UserItemTicket = &apos;{0}&apos; AND ConversationTicket = &apos;{1}&apos;;.
        /// </summary>
        internal static string sqlUserProgressSelectCourse {
            get {
                return ResourceManager.GetString("sqlUserProgressSelectCourse", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT * FROM UserProgress WHERE UserItemTicket = &apos;{0}&apos;;.
        /// </summary>
        internal static string sqlUserProgressSelectUserAll {
            get {
                return ResourceManager.GetString("sqlUserProgressSelectUserAll", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE UserProgress SET ConversationState = {0} WHERE UserProgressTicket = &apos;{1}&apos;;.
        /// </summary>
        internal static string sqlUserProgressUpdateStatus {
            get {
                return ResourceManager.GetString("sqlUserProgressUpdateStatus", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to UPDATE UserProgress SET ConversationState = {0}, UserLevel = {1} WHERE UserProgressTicket = &apos;{2}&apos;;.
        /// </summary>
        internal static string sqlUserProgressUpdateStatusLevel {
            get {
                return ResourceManager.GetString("sqlUserProgressUpdateStatusLevel", resourceCulture);
            }
        }
    }
}
