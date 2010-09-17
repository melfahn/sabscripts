using SABSync.Entities;

namespace SABSync.TvDb
{
    public interface ITvDbService
    {
        void GetEpisodeName(Episode episode);
        TvDbShowInfo GetShowData(string seriesName);
        TvDbEpisodeInfo GetEpisodeData(int episodeId);
        int GetServerTime();
        TvDbUpdates GetUpdates(int time);
        TvDbShowInfo GetShowUpdates(int seriesId);
    }
}