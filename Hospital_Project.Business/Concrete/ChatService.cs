﻿using HospitalProject.Business.Abstract;
using HospitalProject.DataAccess.Abstract;
using HospitalProject.Entities.DbEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HospitalProject.Business.Concrete
{
    public class ChatService : IChatService
    {
        private readonly IChatDal _chatDal;

        public ChatService(IChatDal chatDal)
        {
            _chatDal = chatDal;
        }

        public async Task AddChatAsync(Chat chat)
        {
            await _chatDal.AddAsync(chat);
        }

        public async Task<IEnumerable<Chat>> GetAllUserChats(string userId)
        {
            var chats = await _chatDal.GetListAsync(c => c.SenderUserId == userId || c.ReceiverUserId == userId);

            var uniqueChats = chats.GroupBy(c =>
            {
                string id1 = c.SenderUserId;

                string id2 = c.ReceiverUserId;

                return id1.CompareTo(id2) < 0 ? $"{id1}_{id2}" : $"{id2}_{id1}";
            })
            .Select(group => group.First());

            return uniqueChats;
        }

        public async Task<Chat?> GetChatAsync(string senderUserId, string receiverUserId)
        {
            return await _chatDal.GetAsync(c => c.SenderUserId == senderUserId && c.ReceiverUserId == receiverUserId);
        }

        public async Task<Chat?> GetChatByIdAsync(string chatId)
        {
            return await _chatDal.GetAsync(c => c.Id == chatId);
        }
    }
}
