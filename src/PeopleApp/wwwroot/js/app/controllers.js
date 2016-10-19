// PeopleController does not correspond to a route, so we need to define it explicitly.
App.PeopleController = Ember.ArrayController.extend();

App.PersonController = Ember.ObjectController.extend({
    isEditing: false,
    actions: {
        edit: function () {
            this.set('isEditing', true);
        },
        save: function () {
            this.content.save();
            this.set('isEditing', false);
        },
        cancel: function () {
            this.set('isEditing', false);
            this.content.rollback();
        }
    }
});

App.PersonController.reopenClass({
    genders: ["Male", "Female"]
});