function validation() {
    if ($('#txtForeName').val() == "") {
        alert('ForeName field should not be blank');

        return false;
    }
    if ($('#txtSurName').val() == "") {
        alert('SurName field should not be blank');

        return false;
    }
    if ($('#txtDOB').val() == "") {
        alert('DateOfBirth field should not be blank');

        return false;
    }
    if ($('#ddlGender option:selected').text() == 'Select') {
        alert('Gender field should not be select');

        return false
    }
    if ($('#txtForeName').val().length < 3) {
        alert('Forename value should not less than 3 character');

        return false;
    }
    if (($('#txtForeName').val().length > 50)) {
        alert('Forename value should not greater than 50 character');

        return false;
    }
    if ($('#txtSurName').val().length < 2) {
        alert('SurName value should not less than 2 character ');

        return false;
    }
    if ($('#txtSurName').val().length > 50) {
        alert('SurName value should not greater than 50 character ');

        return false;
    }

    return true;
}

$(document).ready(function () {
    /*-------For Calender Control----*/
    $('#txtDOB').datepicker();
    $('#txtDOB').datepicker('setDate', 'today');

    $(function () {
        $(".date-picker").datepicker({
            changeMonth: true,
            changeYear: true,
            dateFormat: 'dd-M-yy'
        });
    });

    /*---------End---------*/
    /*--------Save Region Start---*/
    $('#btnSave').on("click", function () {
        if (validation() == true) {
            var forename = $('#txtForeName').val();
            var surname = $('#txtSurName').val();
            var dateOfBirth = $('#txtDOB').val();
            var gender = $('#ddlGender option:selected').val();
            var home = $('#txtHome').val();
            var work = $('#txtWork').val();
            var mobile = $('#txtMobile').val();

            var patientDetailsModel = {
                "foreName": "",
                "surname": "",
                "dateOfBirth": "",
                "gender": 0,
                "telephones": []
            };

            var telephoneHome = {
                "phoneType": 0,
                "phoneNumber": home
            };
            var telephoneWork = {
                "phoneType": 1,
                "phoneNumber": work
            };
            var telephoneMobile = {
                "phoneType": 2,
                "phoneNumber": mobile
            };

            var telePhones = [];
            telePhones.push(telephoneHome);
            telePhones.push(telephoneWork);
            telePhones.push(telephoneMobile);

            patientDetailsModel.foreName = forename;
            patientDetailsModel.surname = surname;
            patientDetailsModel.dateOfBirth = dateOfBirth;
            patientDetailsModel.gender = gender;
            patientDetailsModel.telephones = telePhones;

            // Ajax Method for Save Patient Information
            $.ajax({
                url: 'http://localhost/PatientDemographicService/api/patient/',
                type: 'POST',
                data: patientDetailsModel,
                datatype: 'json',
                ContentType: 'application/json; charset=utf-8',
                success: function (response) {
                    alert('Saved Successfully');
                },
                error: function (jqXHR, textStatus, errorThrown) {
                    alert('An error occurred... ');
                }
            });
        }
    });
    /*-------Save Region End-----*/

    /*----------Search Region Start------*/
    $('#btnSearch').on("click", function () {
        /* Ajax method for Call API and data retrive  */
        $.ajax({
            url: 'http://localhost/PatientDemographicService/api/patient/',
            type: 'GET',
            data: '',
            datatype: 'json',
            ContentType: 'application/json; charset=utf-8',
            success: function (data) {
                var gender = "";
                for (var i = 0; i < data.length; i++) {
                    gender = data[i].Gender == 0 ? "Male" : "Female";
                    var dateOfBirth = dateToDMY(new Date(data[i].DateOfBirth));
                    var row = '<tr><td> ' + data[i].ForeName + ' </td> <td> ' + data[i].Surname + ' </td> <td>' + dateOfBirth + '</td> <td>' + gender + '</td> </tr>'
                    $("#tblPatientInfo").append(row);
                }
            },
            error: function (jqXHR, textStatus, errorThrown) {
                alert('An error occurred... ');
            }
        });
    });
    /*-----------Search Region End------*/
});

// function for  convert proper dateformat ex: date/month/year
function dateToDMY(date) {
    var d = date.getDate();
    var m = date.getMonth() + 1; //Month from 0 to 11
    var y = date.getFullYear();

    return (d <= 9 ? '0' + d : d) + '/' + (m <= 9 ? '0' + m : m) + '/' + y;
}

//function for dateformat validation
function Checkdateformat(datecontrol) {
    var dateformat = /^(0?[1-9]|[12][0-9]|3[01])[\/\-](0?[1-9]|1[012])[\/\-]\d{4}$/;
    var Val_date = datecontrol;
    if (Val_date.match(dateformat)) {
        var seperator1 = Val_date.split('/');
        var seperator2 = Val_date.split('-');

        if (seperator1.length > 1) {
            var splitdate = Val_date.split('/');
        }
        else if (seperator2.length > 1) {
            var splitdate = Val_date.split('-');
        }
        var dd = parseInt(splitdate[0]);
        var mm = parseInt(splitdate[1]);
        var yy = parseInt(splitdate[2]);
        var ListofDays = [31, 28, 31, 30, 31, 30, 31, 31, 30, 31, 30, 31];
        if (mm == 1 || mm > 2) {
            if (dd > ListofDays[mm - 1]) {
                alert('Invalid date format!');

                return false;
            }
        }
        if (mm == 2) {
            var lyear = false;
            if ((!(yy % 4) && yy % 100) || !(yy % 400)) {
                lyear = true;
            }
            if ((lyear == false) && (dd >= 29)) {
                alert('Invalid date format!');

                return false;
            }
            if ((lyear == true) && (dd > 29)) {
                alert('Invalid date format!');

                return false;
            }
        }
    }
    else {
        alert("Invalid date format!");

        return false;
    }
}
