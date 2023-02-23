using System;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RazorPagesLab.Pages.AddressBook;

public class EditModel : PageModel
{
	private readonly IMediator _mediator;
	private readonly IRepo<AddressBookEntry> _repo;

	public EditModel(IRepo<AddressBookEntry> repo, IMediator mediator)
	{
		_repo = repo;
		_mediator = mediator;
	}

	[BindProperty]
	public UpdateAddressRequest UpdateAddressRequest { get; set; }

	public void OnGet(Guid id)
	{
		// Todo: Use repo to get address book entry, set UpdateAddressRequest fields.
		var entry = _repo.FindById(id);
		if (entry != null)
		{
			 UpdateAddressRequest = new UpdateAddressRequest()
			{
				Id = entry.Id,
				Line1 = entry.Line1,
				City = entry.City,
				State = entry.State,
				PostalCode = entry.PostalCode,
			};
		}
	}

	public ActionResult OnPost()
	{
		// Todo: Use mediator to send a "command" to update the address book entry, redirect to entry list.
		if (ModelState.IsValid)
		{
            _ = await _mediator.Send(UpdateAddressRequest);
            return RedirectToPage("Index");
        }

		return Page();
	}
}