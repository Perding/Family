using Entity;
using Repository.IReposotiry;
using Service.IService;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Common;
namespace Service.Service
{
    public class ARefundesService: ARefundesIService
    {
        private IARefundesRepository _aRefundesRepository;
        private IAStudentRepository _aStudentRepository;
        public ARefundesService(IARefundesRepository aRefundesRepository, IAStudentRepository aStudentRepository)
        {
            _aStudentRepository = aStudentRepository;
            _aRefundesRepository = aRefundesRepository;
        }
        public async Task<bool> Insert(ARefundes aRefundes)
        {
            Dictionary<object, string> dic = new Dictionary<object, string>();
            try
            {
                string[] arr = aRefundes.str.Split('|');
                if (arr.Length > 0)
                {
                    ARefund m2 = new ARefund();
                    List<ARefundes> vs = new List<ARefundes>();
                    for (int j = 0; j < arr.Length; j++)
                    {
                        vs.Add(new ARefundes());
                    }
                    string[] crr = arr[0].Split(',');
                    if (!string.IsNullOrEmpty(aRefundes.ID))
                    {
                        m2.ID = aRefundes.ID;
                        m2.Node = aRefundes.Note;
                        m2.tDatetime = aRefundes.F_CreatorTime;
                        StringBuilder str = new StringBuilder();
                        str.Append("update ARefund set Node=@Node,tDatetime=@tDatetime where id=@id");
                        dic.Add(m2, str.ToString());
                        ARefundes mo = new ARefundes();
                        mo.ArefundId = aRefundes.ID;
                        dic.Add(mo, "delete ARefundes where ArefundId=@ArefundId");
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(crr[0]))
                        {
                            m2.ID = Guid.NewGuid().ToString();
                            m2.Node = aRefundes.Note;
                            m2.StudID = aRefundes.StudentId;
                            m2.tDatetime = aRefundes.F_CreatorTime;
                            StringBuilder str = new StringBuilder();
                            str.Append("insert into ARefund([ID],StudID,[Node],[tDatetime])");
                            str.Append("values(@ID,@StudID,@Node,@tDatetime)");
                            dic.Add(m2, str.ToString());
                        }
                    }
                   
                    for (int i = 0; i < arr.Length; i++)
                    {
                        string[] brr = arr[i].Split(',');
                        if (!string.IsNullOrEmpty(brr[0]))
                        {
                            vs[i] = new ARefundes();
                            vs[i].ID = Guid.NewGuid().ToString();
                            vs[i].ArefundId = m2.ID;
                            vs[i].F_CreatorTime = aRefundes.F_CreatorTime;
                            vs[i].Category = brr[0];
                            if (brr[0]=="key9")
                            {
                                await _aStudentRepository.UpdateDrop(aRefundes.StudentId);
                            }
                            vs[i].Amount = decimal.Parse(brr[1]);
                           
                            vs[i].F_CreatorUserId = aRefundes.F_CreatorUserId;
                            vs[i].StudentId = aRefundes.StudentId;
                            StringBuilder str = new StringBuilder();
                            str.Append("insert into ARefundes([ID],ArefundId,[StudentId],[Category],[Amount],[F_CreatorTime],[F_CreatorUserId])");
                            str.Append("values(@ID,@ArefundId,@StudentId,@Category,@Amount,@F_CreatorTime,@F_CreatorUserId)");
                            dic.Add(vs[i],str.ToString());
                        }
                    }
                }
               
            }
            catch (Exception ex)
            {
                LogHelp.Error("Insert" + ex.Message);
            }
            return await _aRefundesRepository.GetListSql(dic);
        }

        public async Task<IEnumerable<ARefundes>> Getlist(string stuid,int type)
        {
            return await _aRefundesRepository.Getlist(stuid,type);
        }
        public async Task<IEnumerable<ARefundes>> GetPayList(string id)
        {
            return await _aRefundesRepository.GetPayList(id);
        }
        public async Task<ARefund> GetModel(string id)
        {
            return await _aRefundesRepository.GetModel(id);
        }
    }
}
