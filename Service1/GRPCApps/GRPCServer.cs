using Google.Protobuf.WellKnownTypes;
using Grpc.Core;

namespace Service1.GRPCApps
{
    public class GRPCServer : SayHello.SayHelloBase
    {
        public List<Student> students = new()
        {
            new Student {
                Department = Department.Cse,
                Name = "Ramana"
            },
            new Student
            {
                Department = Department.Ece,
                Name = "Logu"
            }
        };
        public override Task<Information> GetInfo(Detail request, ServerCallContext context)
        {
            return Task.FromResult(new Information
            {
                Name = request.Name,
                Age = 21,
                Rollno = 1001,
                Department = Department.Ece,
                EventDate = DateTime.Now.ToString(),
                StartTime = DateTime.UtcNow.ToTimestamp(),
                Time = DateTime.Now.ToString("hh:mm")
            });
        }
        public override Task<ResponseMessage> GetHelloMessage(Detail request, ServerCallContext context)
        {
            return Task.FromResult(new ResponseMessage
            {
                Message = $"Hi Bro {request.Name}"
            });
        }
        public override Task<Students> GetStudents(Empty request, ServerCallContext context)
        {
            var studentList = new Students();
            foreach (var item in students)
            {
                studentList.Students_.Add(item);
            }
            return Task.FromResult(studentList);
        }
        public override Task<Marks> GetMarks(Empty request, ServerCallContext context)
        {
            var markList = new Marks();
            Dictionary<string, int> keyValuePairs = new Dictionary<string, int>()
            {
                ["Tamil"] = 90,
                ["English"] = 70,
                ["Maths"] = 20,
            };
            for (int i = 0; i < keyValuePairs.Count; i++)
            {
                var dict = new Mark();
                foreach (var item in keyValuePairs)
                {
                    dict.Mark_.Add(item.Key, item.Value+i);
                }
                markList.Marks_.Add(dict);
            }
            return Task.FromResult(markList);
        }
    }
}
