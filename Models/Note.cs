using Jsc.TaskManager.DomainRepositories;
using Jsc.TaskManager.Services;
using System;

namespace Jsc.TaskManager.Models
{
    public class Note : INote
    {
        public DateTime DateTime { get; set; } = DateTime.Now;

        public INoteParent Parent { get; private set; }

        public string Text { get; set; }        

        long IBusinessEntity.Id { get; set; }        
    }    
}