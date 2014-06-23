contactsApp.factory('ContactsDataService', function ($resource) {

    var resource = $resource('api/Contacts/:id',
    { id: "@id" },
    {
        get: { method: 'GET' },
        save: { method: 'PUT' },
        query: { method: 'GET', isArray: true },
        create: { method: 'POST' },
        delete: { method: 'DELETE' }
    });

    return {
        getAllContacts: function () {
            return resource.query();
        },

        getContact: function (contactID) {
        return resource.get(contactID);
        },

        saveContact: function (contactId, contact) {
            return resource.save({ id: contactId }, contact );
        },
        createContact: function (contact) {
            return resource.create(contact);
        },
        deleteContact: function (contact) {
            return resource.delete({ id: contact.id });
        }
    }


});
