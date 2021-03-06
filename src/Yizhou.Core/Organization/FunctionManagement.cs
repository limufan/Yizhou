﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


using Yizhou.Api.Organization;

namespace Yizhou.Core.Organization
{
    public class FunctionManagement
    {
        OrganizationManagement _orgMnger;
        Dictionary<string, Function> _funtions;
        bool _loaded;

        public FunctionManagement(OrganizationManagement orgMnger)
        {
            _loaded = false;
            this._orgMnger = orgMnger;
            _funtions = new Dictionary<string,Function>();
        }

        public void AddFunction(List<Function> functions)
        {
            foreach (Function function in functions)
            {
                this._funtions.Add(function.ID, function);
            }
        }

        public event TEventHandler<FunctionManagement, Function> Created;

        public Function Create(string id, string name, string url, string iconClass, int sort, List<Member> members)
        {
            Function function = new Function(id, name, url, iconClass, sort, members, this._orgMnger);
            this._funtions.Add(function.ID, function);
            if (this.Created != null)
            {
                this.Created(this, function);
            }
            return function;
        }

        public Function GetFunctionById(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return null;
            }
            try
            {
                return this._funtions[id];
            }
            catch (KeyNotFoundException)
            {

            }
            return null;
        }

        public List<Function> GetAllFunction()
        {
            return this._funtions.Values.ToList();
        }
    }
}
