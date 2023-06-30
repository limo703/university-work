package ru.alexashin.springcourse.controllers;

import org.springframework.stereotype.Controller;
import org.springframework.ui.Model;
import org.springframework.web.bind.annotation.*;
import ru.alexashin.springcourse.dao.PersonDAO;
import ru.alexashin.springcourse.models.Person;

@Controller
@RequestMapping("/admin")
public class adminController {

    private final PersonDAO personDAO;


    public adminController(PersonDAO personDAO) {
        this.personDAO = personDAO;
    }

    @GetMapping()
    public String adminPage(Model model, @ModelAttribute("person") Person person){
        model.addAttribute("people", personDAO.index());

        return "adminPage";
    }

    @PatchMapping("/add")
    public String makeAdmin(@ModelAttribute("person")Person person){
        System.out.println(person.getId());
        return "redirect:/people";
    }
}
