﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Yizhou.Core.Organization;
using Yizhou.Data.Organization;

namespace Yizhou.Data.DataProviders
{
    public class PositionDataProvider
    {
        OrganizationManagement _orgManager;
        public PositionDataProvider(OrganizationManagement orgManager)
        {
            this._orgManager = orgManager;
        }

        public void Insert(Position position)
        {
            PositionModel model = new PositionModel
            {
                Name = position.Name,
                ParentId = position.Parent == null ? "" : position.Parent.ID,
                Remark = position.Remark,
                ID = position.ID
            };

            NHibernateHelper.CurrentSession.Save(model);
            NHibernateHelper.CurrentSession.Flush();
        }

        public void Update(Position position)
        {
            PositionModel model = NHibernateHelper.CurrentSession.Get<PositionModel>(position.ID);
            model.Name = position.Name;
            model.Remark = position.Remark;
            model.ParentId = position.Parent == null ? "" : position.Parent.ID;
            model.UserIds = string.Join(",", position.Users.Select(u => u.ID));

            NHibernateHelper.CurrentSession.Update(model);
            NHibernateHelper.CurrentSession.Flush();
        }

        public void Delete(Position position)
        {
            PositionModel model = NHibernateHelper.CurrentSession.Get<PositionModel>(position.ID);
            NHibernateHelper.CurrentSession.Delete(model);
            NHibernateHelper.CurrentSession.Flush();
        }

        public void Load()
        {
            List<Position> positions = new List<Position>();
            List<PositionModel> models = NHibernateHelper.CurrentSession.QueryOver<PositionModel>().List().ToList();
            if (models != null)
            {
                models.ForEach(x =>
                {
                    Position position = new Position(x.ID, x.Name, x.ParentId, x.Remark, this._orgManager);
                    positions.Add(position);
                });
            }
            this._orgManager.PositionManager.AddPosition(positions);
        }

        public void LoadUsers()
        {
            Dictionary<string, string> groupUsers = new Dictionary<string, string>();
            List<PositionModel> models = NHibernateHelper.CurrentSession.QueryOver<PositionModel>().List().ToList();
            foreach (PositionModel model in models)
            {
                groupUsers.Add(model.ID, model.UserIds);
            }
            this._orgManager.PositionManager.Positions.ToList().ForEach(p =>
            {
                string userIds = groupUsers[p.ID];
                if (!string.IsNullOrEmpty(userIds))
                {
                    List<User> users = userIds.Split(',').Select(userId => this._orgManager.UserManager.GetUserById(userId)).ToList();
                    p.AddUser(users);
                }
            });
        }
    }
}
