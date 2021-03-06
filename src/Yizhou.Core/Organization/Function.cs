﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.Collections.ObjectModel;

using Yizhou.Api.Organization;

namespace Yizhou.Core.Organization
{
    public class Function
    {
        OrganizationManagement _orgManager;

        public Function(string id, string name, string url, string iconClass, int sort, List<Member> ownerMembers,  OrganizationManagement orgManager)
        {
            this._orgManager = orgManager;
            this.ID = id;
            this.Name = name;
            this.Url = url;
            this.IconClass = iconClass;
            this.Sort = sort;

            this.OwnerMembers = ownerMembers;
            if (this.OwnerMembers == null)
            {
                this.OwnerMembers = new List<Member>();
            }
        }

        public string ID { private set; get; }

        public string Name { private set; get; }

        public string Url { private set; get; }

        public string IconClass { set; get; }

        public int Sort { private set; get; }

        public List<Member> OwnerMembers { private set; get; }

        public bool HasPermission(User user)
        {
            if (user == null)
            {
                throw new ArgumentNullException("user");
            }
            if (user.Role == UserRole.Administrator)
            {
                return true;
            }
            return this.OwnerMembers.Any(x => x.Contains(user));
        }

        public FunctionInfo Map()
        {
            FunctionInfo info = new FunctionInfo();
            info.ID = this.ID;
            info.Name = this.Name;
            info.Url = this.Url;
            info.Sort = this.Sort;
            info.IconClass = this.IconClass;
            return info;
        }
    }
}
