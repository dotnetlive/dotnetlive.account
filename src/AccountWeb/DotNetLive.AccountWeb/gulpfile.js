/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    es = require('event-stream');

var areas = ["default"];
var bundles = areas.map(function (obj) {
    var webroot = "./wwwroot/" + obj + "/";
    return {
        js: webroot + "js/**/*.js",
        minJs: webroot + "js/**/*.min.js",
        css: webroot + "css/**/*.css",
        minCss: webroot + "css/**/*.min.css",
        concatJsDest: webroot + "js/site.min.js",
        concatCssDest: webroot + "css/site.min.css",
    };
});

gulp.task('clean:js', function () {
    bundles.map(function (obj) {
        rimraf(obj.concatJsDest, (error) => { });
    });
});

gulp.task('clean:css', function () {
    bundles.map(function (obj) {
        rimraf(obj.concatCssDest, (error) => { });
    });
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task('min:js', function () {
    return es.merge(bundles.map(function (obj) {
        return gulp.src([obj.js, "!" + obj.minJs], { base: "." })
            .pipe(concat(obj.concatJsDest))
            .pipe(uglify())
            .pipe(gulp.dest("."));
    }));
});

gulp.task('min:css', function () {
    return es.merge(bundles.map(function (obj) {
        return gulp.src([obj.css, "!" + obj.minCss])
            .pipe(concat(obj.concatCssDest))
            .pipe(cssmin())
            .pipe(gulp.dest("."));
    }));
});

gulp.task("min", ["min:js", "min:css"]);

gulp.task("default", ["clean", "min"]);
