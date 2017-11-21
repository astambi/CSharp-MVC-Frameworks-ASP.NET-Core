namespace CameraBazaar.Web.Controllers
{
    using Data.Models;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.AspNetCore.Mvc;
    using Models.Cameras;
    using Services;
    using System;
    using System.Collections.Generic;

    public class CamerasController : Controller
    {
        private readonly ICameraService cameraService;
        private readonly UserManager<User> userManager;

        public CamerasController(
            ICameraService cameraService,
            UserManager<User> userManager)
        {
            this.cameraService = cameraService;
            this.userManager = userManager;
        }

        [Authorize]
        public IActionResult Add() => this.View();

        [Authorize]
        [HttpPost]
        public IActionResult Add(CameraViewModel cameraModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(cameraModel);
            }

            this.cameraService.Create(
                cameraModel.Make,
                cameraModel.Model,
                cameraModel.Price,
                cameraModel.Quantity,
                cameraModel.MinShutterSpeed,
                cameraModel.MaxShutterSpeed,
                cameraModel.MinISO,
                cameraModel.MaxISO,
                cameraModel.IsFullFrame,
                cameraModel.VideoResolution,
                cameraModel.LightMeterings,
                cameraModel.Description,
                cameraModel.ImageUrl,
                this.userManager.GetUserId(this.User));

            return this.RedirectToAction(nameof(All));
        }

        public IActionResult All()
        {
            var camerasData = this.cameraService.All();

            return this.View(camerasData);
        }

        public IActionResult Details(int id)
        {
            var cameraData = this.cameraService.GetById(id);

            return this.View(cameraData);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            // Owner
            var cameraData = this.cameraService.GetByIdWithSeller(id);

            var currentUsername = this.GetCurrentUsername();
            if (currentUsername != cameraData.SellerUsername)
            {
                return RedirectToAction(
                    nameof(Details),
                    routeValues: new { id = id });
            }

            return this.View(cameraData);
        }

        [Authorize]
        public IActionResult ConfirmDelete(int id, string sellerUsername)
        {
            // Owner
            var currentUsername = this.GetCurrentUsername();
            if (this.GetCurrentUsername() != sellerUsername)
            {
                return RedirectToAction(
                    nameof(Details),
                    routeValues: new { id = id });
            }

            this.cameraService.Delete(id);

            return RedirectToAction(nameof(All));
        }

        [Authorize]
        public IActionResult Edit(int id)
        {
            // Owner
            var cameraData = this.cameraService.GetById(id);

            var currentUsername = this.GetCurrentUsername();
            if (currentUsername != cameraData.UserName)
            {
                return RedirectToAction(nameof(Details), routeValues: new { id = id });
            }

            var cameraModel = new CameraViewModel
            {
                Description = cameraData.Description,
                VideoResolution = cameraData.VideoResolution,
                Quantity = cameraData.Quantity,
                Price = cameraData.Price,
                Model = cameraData.Model,
                ImageUrl = cameraData.ImageUrl,
                IsFullFrame = cameraData.IsFullFrame,
                Make = cameraData.Make,
                MinISO = cameraData.MinISO,
                MaxISO = cameraData.MaxISO,
                MinShutterSpeed = cameraData.MinShutterSpeed,
                MaxShutterSpeed = cameraData.MaxShutterSpeed
            };

            // todo light metering enums selected values

            return this.View(cameraModel);
        }

        [Authorize]
        [HttpPost]
        public IActionResult Edit(int id, CameraViewModel cameraModel)
        {
            if (!this.ModelState.IsValid)
            {
                return this.View(cameraModel);
            }

            // Owner
            var cameraData = this.cameraService.GetByIdWithSeller(id);

            var currentUsername = this.GetCurrentUsername();
            if (currentUsername != cameraData.SellerUsername)
            {
                return RedirectToAction(nameof(Details), routeValues: new { id = id });
            }

            this.cameraService.Update(
                id,
                cameraModel.Make,
                cameraModel.Model,
                cameraModel.Price,
                cameraModel.Quantity,
                cameraModel.MinShutterSpeed,
                cameraModel.MaxShutterSpeed,
                cameraModel.MinISO,
                cameraModel.MaxISO,
                cameraModel.IsFullFrame,
                cameraModel.VideoResolution,
                cameraModel.LightMeterings,
                cameraModel.Description,
                cameraModel.ImageUrl);

            return RedirectToAction(nameof(All));
        }

        private string GetCurrentUsername()
        {
            return this.userManager.GetUserName(this.User);
        }
    }
}
