contactsApp.controller("contactsController",
    function ($scope, $window, ContactsDataService) {
        $scope.sortOrder = "lastName";


        $scope.cancel = function (contact) {
            if (contact)
                contact.editMode = false;
            else
                $scope.addMode = !$scope.addMode;
        }

        $scope.toggleAddMode = function () {
            $scope.addMode = !$scope.addMode;
            if ($scope.addMode == false) {
                $scope.contacts.push($scope.newContact);
                $scope.newContact.img ="anon.jpg"
                ContactsDataService.createContact($scope.newContact)
                $scope.newContact = new Contact();

            }
        };
        $scope.toggleEditMode = function (contact) {
            contact.editMode = !contact.editMode;
            if (contact.editMode) {
                contact.editContact = new Contact(contact);
            } else {
                contact.lastName = contact.editContact.lastName;
                contact.firstName = contact.editContact.firstName;
                contact.address = contact.editContact.address;
                contact.city = contact.editContact.city;
                contact.state = contact.editContact.state;
                contact.img = contact.editContact.img;
                contact.zip = contact.editContact.zip;
                contact.homePhone = contact.editContact.homePhone;
                contact.workPhone = contact.editContact.workPhone;
                contact.cellPhone = contact.editContact.cellPhone;
                ContactsDataService.saveContact(contact.id, contact.editContact);
            }
        };


        $scope.addText = function () {
            return $scope.addMode ? "Save" : "Add";
        };

        $scope.newContact = new Contact();


        //$scope.contacts =
        //    [
        //        new Contact(
        //             "Godzilla",
        //             "Mothra",
        //             "123 Strawberry Fields",
        //             "Forever",
        //             "UK",
        //             12344,
        //             2121112221,
        //             2121112222,
        //             2121112223,
        //            "zilla.jpg"
        //        ),
        //        new Contact(
        //             "Jack",
        //             "Jack",
        //             "123 Strawberry Fields",
        //             "Forever",
        //             "UK",
        //             12344,
        //             2121112221,
        //             2121112222,
        //             2121112223,
        //            "jack.jpg"
        //        ),
        //       new Contact(
        //             "Doge",
        //             "Doge",
        //             "123 Strawberry Fields",
        //             "Forever",
        //             "UK",
        //             12344,
        //             2121112221,
        //             2121112222,
        //             2121112223,
        //           "doge.jpg"
        //        ),
        //      new Contact(
        //             "Michaelangelo",
        //             "Turtle",
        //             "123 Strawberry Fields",
        //             "Forever",
        //             "UK",
        //             12344,
        //             2121112221,
        //             2121112222,
        //             2121112223,
        //             "mike.jpg"
        //        ),
        //    ];

        $scope.deleteContact = function (contact) {
            var index = $scope.contacts.indexOf(contact);
            if ($window.confirm("Are you sure you want to delete?")) {
                $scope.contacts.splice(index, 1);
                ContactsDataService.deleteContact(contact);
            }
               
        };


        $scope.toggleShowDetails = function (contact) {
            contact.showDetails = !contact.showDetails;
        }

        $scope.contacts = ContactsDataService.getAllContacts();
    }
);
