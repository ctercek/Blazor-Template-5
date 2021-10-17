// --------------------------------------------------------------------------------------------------------------------
// <copyright file="Index.razor.cs" company="CompanyName">
//   Copyright 2021
// </copyright>
// <summary>
//   Defines the Index type.
// </summary>
// --------------------------------------------------------------------------------------------------------------------


namespace btp.Pages.User
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using btp.Areas.Identity;
    using btp.Data.Models;

    using Microsoft.AspNetCore.Components;
    using Microsoft.AspNetCore.Components.Forms;

    using Syncfusion.Blazor.Grids;

    /// <summary>
    /// The index.
    /// </summary>
    public partial class Index : ComponentBase
    {
        /// <summary>
        /// The current user info.
        /// </summary>
        private ApplicationUser currentUserInfo;

        /// <summary>
        /// The current user.
        /// </summary>
        private string currentUser;

        /// <summary>
        /// The current user id.
        /// </summary>
        private string currentUserId;

        /// <summary>
        /// The addresses.
        /// </summary>
        private List<AspNetAddress> addresses;

        /// <summary>
        /// The phones.
        /// </summary>
        private List<AspNetPhone> phones;

        /// <summary>
        /// The context.
        /// </summary>
        private EditContext context;

        /// <summary>
        /// The on initialized async.
        /// </summary>
        /// <returns>
        /// The <see cref="Task"/>.
        /// </returns>
        protected override async Task OnInitializedAsync()
        {
            this.currentUser = this.userInfoService.GetCurrentUser();
            this.currentUserId = this.userInfoService.GetCurrentUserId();
            this.currentUserInfo = (ApplicationUser)(await this.userInfoService.GetUsersAsync(this.currentUser)).FirstOrDefault();
            this.addresses = await this.addressService.GetAddressesAsync(this.currentUser);
            this.phones = await this.phoneService.GetPhonesAsync(this.currentUser);
            if (this.currentUserInfo != null)
            {
                this.context = new EditContext(this.currentUserInfo);
            }

        }

        /// <summary>
        /// The valid form submitted user.
        /// </summary>
        /// <param name="context">
        /// The context.
        /// </param>
        public void ValidFormSubmittedUser(EditContext context)
        {

            if (context.Validate())
            {
                Console.WriteLine(context.Model);


            }


        }

        /// <summary>
        /// The action begin handler.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public void ActionBeginHandler(ActionEventArgs<btp.Data.Models.AspNetAddress> args)
        {
            var t = args.Data;

            switch (args.RequestType)
            {
                case Syncfusion.Blazor.Grids.Action.Save:
                    if (args.Action == "Add")
                    {
                        t.UserId = currentUserId;
                        this.addressService.AddAddress(t);
                    }
                    else
                    {
                        t.UserId = currentUserId;
                        this.addressService.UpdateAddress(t);
                    }
                    break;
                case Syncfusion.Blazor.Grids.Action.Delete:
                    this.addressService.RemoveAddress(t);
                    break;
                default:
                    Console.WriteLine(args.RequestType);
                    break;
            }
        }

        /// <summary>
        /// The action begin handler.
        /// </summary>
        /// <param name="args">
        /// The args.
        /// </param>
        public void ActionBeginHandler(ActionEventArgs<btp.Data.Models.AspNetPhone> args)
        {

            var t = args.Data;

            switch (args.RequestType)
            {
                case Syncfusion.Blazor.Grids.Action.Save:
                    if (args.Action == "Add")
                    {
                        t.UserId = currentUserId;
                        this.phoneService.AddPhone(t);
                    }
                    else  //Update
                    {
                        t.UserId = currentUserId;
                        this.phoneService.UpdatePhone(t);
                    }

                    break;
                case Syncfusion.Blazor.Grids.Action.Delete:
                    this.phoneService.RemovePhone(t);
                    break;
                default:
                    Console.WriteLine(args.RequestType);
                    break;
            }
        }


    }




}
