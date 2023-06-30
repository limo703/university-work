package ru.alexashin.springcourse.dao;

import org.hibernate.validator.constraints.pl.PESEL;
import org.springframework.beans.factory.annotation.Autowired;
import org.springframework.jdbc.core.BatchPreparedStatementSetter;
import org.springframework.jdbc.core.BeanPropertyRowMapper;
import org.springframework.jdbc.core.JdbcTemplate;
import org.springframework.stereotype.Component;
import ru.alexashin.springcourse.models.Person;

import java.sql.*;
import java.util.ArrayList;
import java.util.List;
import java.util.Optional;

@Component
public class PersonDAO {
    private final JdbcTemplate jdbcTemplate;

    @Autowired
    public PersonDAO(JdbcTemplate jdbcTemplate){
        this.jdbcTemplate = jdbcTemplate;
    }


    public List<Person> index(){
        return jdbcTemplate.query("SELECT * FROM person",new BeanPropertyRowMapper<>(Person.class));
    }

    public Person show(int id) {
        return jdbcTemplate.query("SELECT * FROM person WHERE id=?", new Object[]{id},new BeanPropertyRowMapper<>(Person.class)).
                stream().findAny().orElse(null);
    }
    public Optional<Person> show(String email){
        return jdbcTemplate.query("SELECT * FROM person WHERE email=?",new Object[]{email},
                new BeanPropertyRowMapper<>(Person.class)).stream().findAny();
    }
    public void save(Person person){
        jdbcTemplate.update("INSERT INTO person(name,age,email,address) VALUES (?,?,?,?)",person.getName(),person.getAge(),person.getEmail(),person.getAddress());
    }
    public void update(int id, Person updatePerson){
        jdbcTemplate.update("UPDATE person SET name=?, age=?,email=?,address=?WHERE id=?",updatePerson.getName(),
                updatePerson.getAge(),updatePerson.getEmail(),updatePerson.getAddress(),id);

    }

    public void delete(int id){
        jdbcTemplate.update("DELETE FROM person WHERE id=?",id);
    }
    ///////
    //тестируем производительность пакетной вставки
    ///

    public void testMultipleUpdate(){
        List<Person> people = create1000People();

        long before = System.currentTimeMillis();

        for(Person person: people){
            jdbcTemplate.update("INSERT INTO person VALUES(?,?,?,?)",person.getId(),person.getName(),person.getAge(),
                    person.getEmail());
        }

        long after = System.currentTimeMillis();
        System.out.println("Time: "+(after-before));
    }
    public void testBatchUpdate(){
        List<Person> people = create1000People();

        long before = System.currentTimeMillis();

        jdbcTemplate.batchUpdate("INSERT INTO person VALUES(?,?,?,?)", new BatchPreparedStatementSetter() {
            @Override
            public void setValues(PreparedStatement ps, int i) throws SQLException {
                ps.setInt(1,people.get(i).getId());
                ps.setString(2,people.get(i).getName());
                ps.setInt(3,people.get(i).getAge());
                ps.setString(4,people.get(i).getEmail());
            }

            @Override
            public int getBatchSize() {
                return people.size();
            }
        });

        long after = System.currentTimeMillis();
        System.out.println("Time"+(after-before));
    }

    private List<Person> create1000People() {
        List<Person> people = new ArrayList<>();
        for(int i = 0; i<1000;i++){
            people.add(new Person(i,"Name"+i,"test"+i+"@mail.com",30,""));
        }
        return people;
    }
}
