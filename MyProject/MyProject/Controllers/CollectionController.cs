﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MyProject.DB;
using MyProject.Models;
using MyProject.ViewModels;

namespace MyProject.Controllers
{
    [Authorize]
    public class CollectionController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly CollectionRepository _collectionRepository;
        private readonly VocabularyRepository _vocabularyRepository;

        public CollectionController(UserManager<User> userManager, CollectionRepository collectionRepository, 
            VocabularyRepository vocabularyRepository)
        {
            _userManager = userManager;
            _collectionRepository = collectionRepository;
            _vocabularyRepository = vocabularyRepository;
        }

        public IActionResult Index()
        {
            return View(_collectionRepository.GetUserCollections(GetCurrentUserId()));
        }

        [HttpGet]
        public IActionResult Create()
        {
            var model = new CreateCollectionViewModel { Vocabularies = _vocabularyRepository.GetAllVocabularies() };
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync(CreateCollectionViewModel model)
        {
            var collection = new Collection
            {
                Name = model.CollectionName,
                VocabularyId = model.VocabularyId,
                UserId = GetCurrentUserId()
            };
            await _collectionRepository.CreateCollectionAsync(collection);
            return RedirectToAction("Index");
        }

        public IActionResult Delete(int id)
        {
            _collectionRepository.DeleteCollection(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> ShowCollectionAsync(int id)
        {
            return View(await _collectionRepository.GetCollectionAsync(id));
        }

        private string GetCurrentUserId()
        {
            return _userManager.GetUserId(HttpContext.User);
        }
    }
}