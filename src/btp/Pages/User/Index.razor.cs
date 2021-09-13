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

    public partial class Index : ComponentBase
    {

        private ApplicationUser _currentUserInfo;
        private string _currentUser;
        private string _currentUserId;
        private List<AspNetAddress> _addresses;
        private List<AspNetPhone> _phones;
        private EditContext _context;

        protected override async Task OnInitializedAsync()
        {
            //_currentUser = UserInfoService.GetCurrentUser();
            //_currentUserId = UserInfoService.GetCurrentUserId();
            //_currentUserInfo = (ApplicationUser)(await this.UserInfoService.GetUsersAsync(this._currentUser)).FirstOrDefault();
            //_addresses = await AddressService.GetAddressesAsync(_currentUser);
            //_phones = await PhoneService.GetPhonesAsync(_currentUser);
            //_context = new EditContext(_currentUserInfo);
        }

        public void ValidFormSubmittedUser(EditContext context)
        {
            //This will need to be limited to another model, otherwise it will send everything back to the client... No bueno!


            if (context.Validate())
            {
                Console.WriteLine(context.Model);


            }


        }

        public void ActionBeginHandler(ActionEventArgs<btp.Data.Models.AspNetAddress> args)
        {
            //var t = args.Data;

            //switch (args.RequestType)
            //{
            //    case Syncfusion.Blazor.Grids.Action.Save:
            //        if (args.Action == "Add")
            //        {
            //            t.UserId = _currentUserId;
            //            AddressService.AddAddress(t);
            //        }
            //        else
            //        {
            //            t.UserId = _currentUserId;
            //            AddressService.UpdateAddress(t);
            //        }
            //        break;
            //    case Syncfusion.Blazor.Grids.Action.Delete:
            //        AddressService.RemoveAddress(t);
            //        break;
            //    default:
            //        Console.WriteLine(args.RequestType);
            //        break;

            //}
        }

        public void ActionBeginHandler(ActionEventArgs<btp.Data.Models.AspNetPhone> args)
        {

            //var t = args.Data;

            //switch (args.RequestType)
            //{
            //    case Syncfusion.Blazor.Grids.Action.Save:
            //        if (args.Action == "Add")
            //        {
            //            t.UserId = _currentUserId;
            //            PhoneService.AddPhone(t);
            //        }
            //        else  //Update
            //        {
            //            t.UserId = _currentUserId;
            //            PhoneService.UpdatePhone(t);
            //        }

            //        break;
            //    case Syncfusion.Blazor.Grids.Action.Delete:
            //        PhoneService.RemovePhone(t);
            //        break;
            //    default:
            //        Console.WriteLine(args.RequestType);
            //        break;
            //}
        }


    }




}
