"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var TagCloudComponent = (function () {
    function TagCloudComponent() {
        this.words = [
            { text: "Lorem", weight: 13, link: 'http://github.com/mistic100/jQCloud' },
            { text: "Ipsum", weight: 10.5, link: 'http://www.strangeplanet.fr' },
            { text: "Dolor", weight: 9.4, link: 'http://piwigo.org' },
        ];
    }
    return TagCloudComponent;
}());
TagCloudComponent = __decorate([
    core_1.Component({
        selector: 'tagcloud',
        templateUrl: './tagcloud.component.html'
    }),
    __metadata("design:paramtypes", [])
], TagCloudComponent);
exports.TagCloudComponent = TagCloudComponent;
//# sourceMappingURL=tagcloud.component.js.map