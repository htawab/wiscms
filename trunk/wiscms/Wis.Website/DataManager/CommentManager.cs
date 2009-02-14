using System;
using System.Text;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;

namespace Wis.Website.DataManager
{
    public class CommentManager
    {
        public CommentManager()
        {
            DbProviderHelper.GetConnection();
        }

        public List<Comment> GetComments()
        {
            List<Comment> comments = new List<Comment>();
            DbCommand command = DbProviderHelper.CreateCommand("SELECTComments", CommandType.StoredProcedure);
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            while (dataReader.Read())
            {
                Comment comment = new Comment();
                comment.CommentId = Convert.ToInt32(dataReader["CommentId"]);

                if (dataReader["CommentGuid"] != DBNull.Value)
                    comment.CommentGuid = (Guid)dataReader["CommentGuid"];
                comment.SubmissionGuid = (Guid)dataReader["SubmissionGuid"];

                if (dataReader["Commentator"] != DBNull.Value)
                    comment.Commentator = Convert.ToString(dataReader["Commentator"]);
                comment.Title = Convert.ToString(dataReader["Title"]);

                if (dataReader["ContentHtml"] != DBNull.Value)
                    comment.ContentHtml = Convert.ToString(dataReader["ContentHtml"]);

                if (dataReader["Original"] != DBNull.Value)
                    comment.Original = Convert.ToString(dataReader["Original"]);
                comment.IPAddress = Convert.ToString(dataReader["IPAddress"]);

                if (dataReader["DateCreated"] != DBNull.Value)
                    comment.DateCreated = Convert.ToDateTime(dataReader["DateCreated"]);
                comments.Add(comment);
            }
            dataReader.Close();
            return comments;
        }


        public static List<Comment> GetCommentsBySubmissionGuid(Guid submissionGuid)
        {
            List<Comment> comments = new List<Comment>();
            DbCommand command = DbProviderHelper.CreateCommand("SelectCommentsBySubmissionGuid", CommandType.StoredProcedure);
            command.Parameters.Add(DbProviderHelper.CreateParameter("@SubmissionGuid", DbType.Guid, submissionGuid));
            DbDataReader dataReader = DbProviderHelper.ExecuteReader(command);
            while (dataReader.Read())
            {
                Comment comment = new Comment();
                comment.CommentId = Convert.ToInt32(dataReader["CommentId"]);

                if (dataReader["CommentGuid"] != DBNull.Value)
                    comment.CommentGuid = (Guid)dataReader["CommentGuid"];
                comment.SubmissionGuid = (Guid)dataReader["SubmissionGuid"];

                if (dataReader["Commentator"] != DBNull.Value)
                    comment.Commentator = Convert.ToString(dataReader["Commentator"]);
                comment.Title = Convert.ToString(dataReader["Title"]);

                if (dataReader["ContentHtml"] != DBNull.Value)
                    comment.ContentHtml = Convert.ToString(dataReader["ContentHtml"]);

                if (dataReader["Original"] != DBNull.Value)
                    comment.Original = Convert.ToString(dataReader["Original"]);
                comment.IPAddress = Convert.ToString(dataReader["IPAddress"]);

                if (dataReader["DateCreated"] != DBNull.Value)
                    comment.DateCreated = Convert.ToDateTime(dataReader["DateCreated"]);
                comments.Add(comment);
            }
            dataReader.Close();
            return comments;
        }


        public Comment GetComment(int CommentId)
        {
            Comment oComment = new Comment();
            DbCommand oDbCommand = DbProviderHelper.CreateCommand("SELECTComment", CommandType.StoredProcedure);
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@CommentId", DbType.Int32, CommentId));
            DbDataReader oDbDataReader = DbProviderHelper.ExecuteReader(oDbCommand);
            while (oDbDataReader.Read())
            {
                oComment.CommentId = Convert.ToInt32(oDbDataReader["CommentId"]);

                if (oDbDataReader["CommentGuid"] != DBNull.Value)
                    oComment.CommentGuid = (Guid)oDbDataReader["CommentGuid"];
                oComment.SubmissionGuid = (Guid)oDbDataReader["SubmissionGuid"];

                if (oDbDataReader["Commentator"] != DBNull.Value)
                    oComment.Commentator = Convert.ToString(oDbDataReader["Commentator"]);
                oComment.Title = Convert.ToString(oDbDataReader["Title"]);

                if (oDbDataReader["ContentHtml"] != DBNull.Value)
                    oComment.ContentHtml = Convert.ToString(oDbDataReader["ContentHtml"]);

                if (oDbDataReader["Original"] != DBNull.Value)
                    oComment.Original = Convert.ToString(oDbDataReader["Original"]);
                oComment.IPAddress = Convert.ToString(oDbDataReader["IPAddress"]);

                if (oDbDataReader["DateCreated"] != DBNull.Value)
                    oComment.DateCreated = Convert.ToDateTime(oDbDataReader["DateCreated"]);
            }
            oDbDataReader.Close();
            return oComment;
        }

        /// <summary>
        /// 添加评论
        /// </summary>
        /// <param name="comment">评论实体类</param>
        /// <returns>返回受影响的记录数</returns>
        public int AddNew(Comment comment)
        {
            DbCommand command = DbProviderHelper.CreateCommand("INSERTComment", CommandType.StoredProcedure);
            
            if (comment.CommentGuid.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@CommentGuid", DbType.Guid, comment.CommentGuid));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@CommentGuid", DbType.Guid, DBNull.Value));
            
            command.Parameters.Add(DbProviderHelper.CreateParameter("@SubmissionGuid", DbType.Guid, comment.SubmissionGuid));

            if (comment.Commentator != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Commentator", DbType.String, comment.Commentator));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Commentator", DbType.String, DBNull.Value));
            
            command.Parameters.Add(DbProviderHelper.CreateParameter("@Title", DbType.String, comment.Title));

            if (comment.ContentHtml != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml", DbType.String, comment.ContentHtml));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml", DbType.String, DBNull.Value));

            if (comment.Original != null)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Original", DbType.String, comment.Original));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@Original", DbType.String, DBNull.Value));
            
            command.Parameters.Add(DbProviderHelper.CreateParameter("@IPAddress", DbType.String, comment.IPAddress));

            if (comment.DateCreated.HasValue)
                command.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated", DbType.DateTime, comment.DateCreated));
            else
                command.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated", DbType.DateTime, DBNull.Value));

            return DbProviderHelper.ExecuteNonQuery(command);
        }

        public int Update(int CommentId, Nullable<Guid> CommentGuid, Guid SubmissionGuid, string Commentator, string Title, string ContentHtml, string Original, string IPAddress, Nullable<DateTime> DateCreated)
        {

            DbCommand oDbCommand = DbProviderHelper.CreateCommand("UPDATEComment", CommandType.StoredProcedure);
            if (CommentGuid.HasValue)
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@CommentGuid", DbType.Guid, CommentGuid));
            else
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@CommentGuid", DbType.Guid, DBNull.Value));
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@SubmissionGuid", DbType.Guid, SubmissionGuid));
            if (Commentator != null)
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Commentator", DbType.String, Commentator));
            else
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Commentator", DbType.String, DBNull.Value));
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Title", DbType.String, Title));
            if (ContentHtml != null)
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml", DbType.String, ContentHtml));
            else
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@ContentHtml", DbType.String, DBNull.Value));
            if (Original != null)
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Original", DbType.String, Original));
            else
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@Original", DbType.String, DBNull.Value));
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@IPAddress", DbType.String, IPAddress));
            if (DateCreated.HasValue)
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated", DbType.DateTime, DateCreated));
            else
                oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@DateCreated", DbType.DateTime, DBNull.Value));
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@CommentId", DbType.Int32, CommentId));
            return DbProviderHelper.ExecuteNonQuery(oDbCommand);
        }

        public int Remove(int CommentId)
        {
            DbCommand oDbCommand = DbProviderHelper.CreateCommand("DELETEComment", CommandType.StoredProcedure);
            oDbCommand.Parameters.Add(DbProviderHelper.CreateParameter("@CommentId", DbType.Int32, CommentId));
            return DbProviderHelper.ExecuteNonQuery(oDbCommand);
        }
    }
}
