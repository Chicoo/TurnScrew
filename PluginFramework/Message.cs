
using System;
using System.Collections.Generic;


namespace TurnScrew.Wiki.PluginFramework
{

    /// <summary>
    /// Represents a Page Discussion Message.
    /// </summary>
    public class Message
    {

        /// <summary>
        /// Initializes a new instance of the <b>Message</b> class.
        /// </summary>
        /// <param name="id">The ID of the Message.</param>
        /// <param name="username">The Username of the User.</param>
        /// <param name="subject">The Subject of the Message.</param>
        /// <param name="dateTime">The Date/Time of the Message.</param>
        /// <param name="body">The body of the Message.</param>
        public Message(int id, string username, string subject, DateTime dateTime, string body)
        {
            ID = id;
            Username = username;
            Subject = subject;
            DateTime = dateTime;
            Body = body;
        }

        /// <summary>
        /// Gets or sets the Message ID.
        /// </summary>
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets the Username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the Subject.
        /// </summary>
        public string Subject { get; set; }

        /// <summary>
        /// Gets or sets the Date/Time.
        /// </summary>
        public DateTime DateTime { get; set; }

        /// <summary>
        /// Gets or sets the Body.
        /// </summary>
        public string Body { get; set; }

        /// <summary>
        /// Gets or sets the Replies.
        /// </summary>
        public Message[] Replies { get; set; } = new Message[0];

    }

    /// <summary>
    /// Compares two Message object using their Date/Time as parameter.
    /// </summary>
    public class MessageDateTimeComparer : IComparer<Message>
    {

        bool reverse = false;

        /// <summary>
        /// Initializes a new instance of the <b>MessageDateTimeComparer</b> class.
        /// </summary>
        /// <param name="reverse">True to compare in reverse order (bigger to smaller).</param>
        public MessageDateTimeComparer(bool reverse)
        {
            this.reverse = reverse;
        }

        /// <summary>
        /// Compares two Message objects.
        /// </summary>
        /// <param name="x">The first object.</param>
        /// <param name="y">The second object.</param>
        /// <returns>The result of the comparison (1, 0 or -1).</returns>
        public int Compare(Message x, Message y)
        {
            if (!reverse) return x.DateTime.CompareTo(y.DateTime);
            else return y.DateTime.CompareTo(x.DateTime);
        }
    }

}
