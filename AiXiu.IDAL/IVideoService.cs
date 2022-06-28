using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AiXiu.IDAL
{
    public interface IVideoService
    {
        bool AddVideo(string VideoId, int UserId, string Headline, string Location);
    }
}
