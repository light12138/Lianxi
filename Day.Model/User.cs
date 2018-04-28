using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Wss.Model
{
    public class User
    {
        [DisplayName("姓名")]
        public string Name { get; set; }
        [DisplayName("性别")]
        public string Sex { get; set; }
        [DisplayName("年龄")]
        public int Age { get; set; }

        
        public User(string name,string sex,int age)
        {
            this.Name = name;
            this.Sex = sex;
            this.Age = age;
        }

        public User()
        {

        }
        
    }

    public class Users {

        public List<User> List { get; set; }


        public Users() {
            List = new List<User> {
                new User("小明","男",10),
                new User("小红","女",10),
                new User("小刚","男",10)
            };
        }
    }
}
