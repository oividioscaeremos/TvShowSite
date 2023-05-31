using TvShowSite.Data.Common;
using TvShowSite.Data.Common.Connections;
using TvShowSite.Domain.Entities.CommentEntities;
using TvShowSite.Domain.TableEntities;

namespace TvShowSite.Data.Repositories
{
    public class CommentRepository : BaseRepository<Comment>
    {
        public CommentRepository(SiteDbConnection connection) : base(connection)
        {

        }

        public async Task<IEnumerable<GetCommentsResponseEntity>> GetCommentsByShowAndEpisodeIdAsync(int showId, int? episodeId, int pageSize, int pageIndex, int userId)
        {
            return await QueryAsync<GetCommentsResponseEntity>($@"
				SELECT * FROM (
					SELECT 
						C.Id,
						C.CommentText,
						UT.Id as UserId,
						UT.UserName,
						UT.ProfilePictureUrl as UserProfilePicture,
						C.InsertDate as CommentDate,
						CASE
							WHEN C.InsertedBy = @UserId THEN 1
							ELSE 0
						END IsUsersComment
					FROM
						site.Comment C,
						site.UserTable UT
					WHERE C.ParentCommentId IS NULL
					AND C.ShowId = @ShowId {(episodeId.HasValue ? "AND C.EpisodeId = @EpisodeId" : "")}
					AND C.InsertedBy = UT.Id
					AND C.IsDeleted <> TRUE
				) X
				ORDER BY X.IsUsersComment DESC, X.Id DESC
				OFFSET @Offset
				LIMIT @Limit
			", new Dictionary<string, object>()
			{
				{ "UserId", userId },
				{ "ShowId", showId },
				{ "EpisodeId", episodeId ?? -1 },
				{ "Offset", (pageIndex - 1) * pageSize },
				{ "Limit", pageSize }
			});
        }

		public async Task<IEnumerable<GetCommentsResponseEntity>> GetChildCommentsByParentCommentIdAsync(int commentId, int userId)
		{
            return await QueryAsync<GetCommentsResponseEntity>($@"
				SELECT * FROM (
					SELECT 
						C.Id,
						C.CommentText,
						UT.Id as UserId,
						UT.UserName,
						UT.ProfilePictureUrl as UserProfilePicture,
						C.InsertDate as CommentDate,
						CASE
							WHEN C.InsertedBy = @UserId THEN 1
							ELSE 0
						END IsUsersComment
					FROM
						site.Comment C,
						site.UserTable UT
					WHERE C.ParentCommentId = @ParentCommentId
					AND C.InsertedBy = UT.Id
					AND C.IsDeleted <> TRUE
				) X
				ORDER BY X.IsUsersComment ASC, X.CommentDate ASC
			", new Dictionary<string, object>()
            {
                { "ParentCommentId", commentId },
				{ "UserId", userId },
            });
        }

		public async Task<IEnumerable<GetLatestCommentsResponseEntity>> GetLatestCommentsAsync(int userId)
		{
			return await QueryAsync<GetLatestCommentsResponseEntity>(@"
				SELECT
					S.Id as ShowId,
					EP.Id as EpisodeId,
					S.Name as ShowName,
					SE.SeasonNumber,
					EP.EpisodeNumber,
					C.CommentText,
					C.InsertDate as CommentDate,
					UT.Username
				FROM
					site.Comment C
					INNER JOIN site.Show S
					ON S.Id = C.ShowId
					LEFT OUTER JOIN site.Episode EP
					ON EP.ShowId = S.Id
					AND EP.Id = C.EpisodeId
					LEFT OUTER JOIN site.Season SE
					ON SE.Id = EP.SeasonId
					INNER JOIN site.UserTable UT
					ON UT.Id = C.InsertedBy
				WHERE
					C.InsertedBy = @UserId
				ORDER BY C.Id DESC
				LIMIT 10
			", new Dictionary<string, object>()
			{
				{ "UserId", userId }
			});
		}
    }
}
