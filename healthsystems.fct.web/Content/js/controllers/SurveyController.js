app.controller("SurveyController", function ($scope, $http, API_URL, ApiFactory) {

    ApiFactory.get("Registration").then(function success(response) {
        $scope.facilities = response;
    }, handleError);

    $scope.uncheckable = false;
    $scope.hasError = false;

    $scope.survey = {
        "id" : 1,
        "Title": "Patient Experience Survey",
        "Name": "",
        "Surname": "",
        "EmailAddress": "",
        "MobileNumber": "",
        "RegistrationId": 0,
        "Categories" : [
            {
                "Title" : "General Cleanliness",
                "Questions" : [
                    {
                        Query : "How likely is it that you would recommend this company to a friend or colleague?",
                        Rating : 0
                    }
                ]
            },
            {
                "Title" : "Serenity",
                "Questions" : [
                    {
                        Query : "Rate how often the area around your room or consulting room was quiet at night or day time?",
                        Rating : 0
                    }
                ]
            },
            {
                "Title" : "Waiting time",
                "Questions" : [
                    {
                        Query : "Rate your level of satisfaction with the length of time it took to see the doctor",
                        Rating : 0
                    },
                    {
                        Query : "Rate your level of satisfaction with the amount of time you had to wait on the day of your appointment",
                        Rating : 0
                    },
                    {
                        Query : "Rate your level of satisfaction with the length of time you had to wait to be admitted for a planned procedure eg stitch removal, pharmacy, lab etc",
                        Rating : 0
                    },
                    {
                        Query : "Rate the waiting area in terms of overall comfort eg seating arrangement, air-conditioning, orderliness etc",
                        Rating : 0
                    }
                ]
            },
            {
                "Title" : "Attitude of doctor(s)",
                "Questions" : [
                    {
                        Query : "How would you rate your doctor’s attitude in terms of treating you with dignity and privacy?",
                        Rating : 0
                    },
                    {
                        Query : "Rate how doctors listened carefully to you and explained things in a way you could understand",
                        Rating : 0
                    }
                ]
            },
            {
                "Title" : "Attitude of nurse(s)",
                "Questions" : [
                    {
                        Query : "How would you rate your nurse’s attitude in terms of treating you with dignity and privacy?",
                        Rating : 0
                    },
                    {
                        Query : "Rate how nurses listened carefully to you and explained things in a way you could understand?",
                        Rating : 0
                    }
                ]
            },
            {
                "Title" : "Attitude of other hospital staff",
                "Questions" : [
                    {
                        Query : "How would you rate other staff’s attitude in terms of treating you with dignity and privacy?",
                        Rating : 0
                    },
                    {
                        Query : "Rate how often you got help as soon as you wanted it?",
                        Rating : 0
                    }
                ]
            },
            {
                "Title" : "General patient Counseling/ courtesy",
                "Questions" : [
                    {
                        Query : "If you had any tests, like a scan or blood tests, how would you rate the hospital for explaining the purpose, the risks, and timescales for getting the results?",
                        Rating : 0
                    },
                    {
                        Query : "Rate how well the medical staff explained your diagnosis to you.",
                        Rating : 0
                    },
                    {
                        Query : "How would you rate the hospital's explanation of what treatment was planned, the risks, outcomes and recovery process?",
                        Rating : 0
                    },
                    {
                        Query : "Before giving you any new medicine, how often did hospital staff tell you what the medicine was for including the side effects in a way you could understand?",
                        Rating : 0
                    },
                    {
                        Query : "How would you rate the advice and information given to you upon leaving hospital?",
                        Rating : 0
                    }
                ]
            },
            {
                "Title" : "Pain control",
                "Questions" : [
                    {
                        Query : "Rate how often your pain was well controlled?  and how often the hospital staff did everything they could to help you with your pain?",
                        Rating : 0
                    }
                ]
            },
            {
                "Title" : "Risk of acquiring hospital Infection",
                "Questions" : [
                    {
                        Query : "Did you contract another infection while in hospital?",
                        Rating : 0
                    }
                ]
            },
            {
                "Title" : "Hospital Experience",
                "Questions" : [
                    {
                        Query : "Do you feel there was adequate staff to look after patient needs?",
                        Rating : 0
                    },
                    {
                        Query : "Rate how well run you thought the hospital was",
                        Rating : 0
                    },
                    {
                        Query : "Rate the quality of care you received",
                        Rating : 0
                    }
                ]
            }
        ]
    };

    $('#myForm').validator().on('submit', function (e) {
        if (e.isDefaultPrevented()) {
            // handle the invalid form...
        } else {

            ApiFactory.post("Survey", $scope.survey).then(function success(response) {

                if (response.data.Error) {
                    toastr["warning"](response.data.ErrorMessage);
                }
                else {
                    $scope.model = response.data;

                    $('#modal-success').modal({ backdrop: 'static' });
                    $('#modal-success').modal('show');

                }

            }, handleError);
        }
    })


    $scope.submitSurvey = function () {

        $("#myForm").submit();
    }





});